using GameEngine.Exception;
using GameEngine.Utilities;
using GameEngine.World.Map.Parser.Lua.Metadata;
using GameEngine.World.Map.Parser.Lua.Sounds;
using GameEngine.World.Map.Parser.Lua.Tiles;
using GameEngine.World.Map.Parser.Lua.Triggers;
using GameEngine.World.Map.Triggers;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua
{
    public class LuaMapLoader
    {
        private Script? _map;
        private LuaTriggerBinder _triggers;

        private LuaMetadataParser _metadata = new();
        private LuaTilesetsParser _tilesets = new();
        private LuaTileParser _tile = new();
        private LuaTriggerGroupParser _triggerGroups = new();
        private LuaSoundParser _sound = new();

        public LuaMapLoader(LuaTriggerBinder triggers)
        {
            _triggers = triggers;
        }

        public MapData Load(string path)
        {
            if(!File.Exists(path))
                throw new MapNotFoundException($"Map not found: {path}");
            
            try
            {
                initializeScript(path);

                var mapTable = executeScript();

                return parseMapData(mapTable);
            }
            catch(ScriptRuntimeException e)
            {
                Log.Error($"Failed to load map: {e.DecoratedMessage}");

                throw;
            }
        }

        private void initializeScript(string path)
        {
            // sandbox map to keep it from touching OS
            var modules = CoreModules.Preset_HardSandbox | CoreModules.ErrorHandling;
            _map = new Script(modules);

            _triggers.Bind(_map);

            var data = _map.LoadFile(path);

            _map.Call(data);
        }

        private Table executeScript()
        {
            if(_map == null)
                throw new InvalidOperationException("Map not initialized");

            var mainFunction = _map?.Globals.Get("main");

            if(mainFunction == null || mainFunction.Type != DataType.Function)
                throw new InvalidOperationException("Map missing main()");

            var result = _map!.Call(mainFunction);

            if(result.Type != DataType.Table)
                throw new InvalidOperationException("main() must return a table");

            return result.Table;
        }

        private MapData parseMapData(Table table)
        {
            var lua = new LuaTableReader(table);

            var metadata = _metadata.Parse(lua.Table("metadata"));
            var mapWidth = metadata.Width;
            var mapHeight = metadata.Height;

            if(metadata?.Width == null || metadata?.Height == null)
                throw new InvalidOperationException("Map size not found when parsing");

            var tilesets = _tilesets.Parse(lua.Table("tilesets"));
            var tiles = _tile.Parse(lua.Table("tiles"), mapWidth!.Value, mapHeight!.Value);
            var triggerGroups = _triggerGroups.Parse(lua.Table("triggerGroups"));
            var sounds = _sound.Parse(lua.Table("sounds"));

            return new MapData
            {
                Metadata = metadata,
                Tilesets = tilesets,
                Tiles = tiles,
                TriggerGroups = triggerGroups,
                Sounds = sounds
            };
        }
    }
}