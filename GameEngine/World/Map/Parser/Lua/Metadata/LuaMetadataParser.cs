using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua.Metadata
{
    public class LuaMetadataParser : ILuaParseTable<MapMetadata>
    {
        private readonly LuaPlayerParser _player = new();
        private readonly LuaLocationParser _location = new();

        public MapMetadata Parse(Table table)
        {
            var lua = new LuaTableReader(table);

            return new MapMetadata
            {
                Title = lua.String("title"),
                Description = lua.String("description"),
                Author = lua.String("author"),
                Width = lua.Int("width"),
                Height = lua.Int("height"),
                Players = _player.Parse(lua.Table("players")),
                Locations = _location.Parse(lua.Table("locations"))
            };
        }
    }
}