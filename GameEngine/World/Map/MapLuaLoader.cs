using GameEngine.Exception;
using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Map.Triggers.Actions;
using GameEngine.World.Map.Triggers.Conditions;
using GameEngine.World.Player;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map
{
    public class MapLuaLoader
    {
        private Script? _mapScript;
        private TriggerLuaBinder _triggers;

        public MapLuaLoader(TriggerLuaBinder triggers)
        {
            _triggers = triggers;
        }

        public MapData Load(string path)
        {
            if(!File.Exists(path))
                throw new MapNotFoundException($"Map not found: {path}");
            
            try
            {
                var modules = CoreModules.Preset_HardSandbox | CoreModules.ErrorHandling;
                _mapScript = new Script(modules);

                _triggers.Bind(_mapScript);

                var dataChunk = _mapScript.LoadFile(path);
                _mapScript.Call(dataChunk);

                var mainFunction = _mapScript.Globals.Get("main");
                if(mainFunction.Type != DataType.Function)
                    throw new InvalidOperationException($"Map missing main()");
                
                var mapData = _mapScript.Call(mainFunction);

                return parseMapData(mapData.Table);
            }
            catch (ScriptRuntimeException e)
            {
                Log.Error(e.DecoratedMessage);
                
                throw;
            }
        }

        private MapData parseMapData(Table mapData)
        {
            var metadata = parseMetadata(mapData.Get("metadata").Table);
            var tiles = parseTiles(mapData.Get("tiles"), metadata.Width!.Value, metadata.Height!.Value);
            var triggerGroups = parseTriggerGroups(mapData.Get("triggerGroups").Table);

            return new MapData
            {
                Metadata = metadata,
                Tiles = tiles,
                TriggerGroups = triggerGroups
            };
        }

        private MapMetadata parseMetadata(Table metadata)
        {
            return new MapMetadata
            {
                Title = metadata.Get("title").String,
                Description = metadata.Get("description").String,
                Author = metadata.Get("author").String,
                Width = (int)metadata.Get("width").Number,
                Height = (int)metadata.Get("height").Number,
                Players = parsePlayers(metadata.Get("players").Table),
                Locations = parseLocations(metadata.Get("locations").Table)
            };
        }

        private List<PlayerData> parsePlayers(Table players)
        {
            var playerList = new List<PlayerData>();

            if(players == null)
                throw new MapPlayersUndefinedException($"Players undefined in map.");
            
            foreach(var pairing in players.Pairs)
            {
                var playerTable = pairing.Value.Table;

                playerList.Add(new PlayerData
                {
                    Name = pairing.Key.String,
                    Id = (int)playerTable.Get("id").Number,
                    Color = playerTable.Get("color").String,
                    IsHuman = playerTable.Get("human").Boolean
                });
            }

            return playerList;
        }

        private List<Location> parseLocations(Table locations)
        {
            var locationList = new List<Location>();

            foreach(var pairing in locations.Pairs)
            {
                var locationTable = pairing.Value.Table;

                locationList.Add(new Location
                {
                    Name = locationTable.Get("name").String,
                    Tiles = parseRectangle(locationTable.Get("tiles").Table),
                    Color = Color.FromString(locationTable.Get("color").String)
                });
            }

            return locationList;
        }

                private List<TriggerGroup> parseTriggerGroups(Table? triggerGroups)
        {
            var groupList = new List<TriggerGroup>();
 
            if(triggerGroups == null)
                return groupList;
 
            foreach(var pairing in triggerGroups.Pairs)
            {
                var groupTable = pairing.Value.Table;
                if(groupTable == null) continue;
 
                var group = new TriggerGroup(
                    groupTable.Get("name").String,
                    groupTable.Get("description").String
                )
                {
                    IsEnabled = groupTable.Get("enabled").Type != DataType.Boolean
                        || groupTable.Get("enabled").Boolean
                };
 
                group.Triggers.AddRange(parseTriggers(groupTable.Get("triggers").Table));
 
                groupList.Add(group);
            }
 
            return groupList;
        }
 
        private List<Trigger> parseTriggers(Table? triggers)
        {
            var triggerList = new List<Trigger>();
 
            if(triggers == null)
                return triggerList;
 
            foreach(var pairing in triggers.Pairs)
            {
                var triggerTable = pairing.Value.Table;
                if(triggerTable == null) continue;
 
                var trigger = new Trigger
                {
                    Name = triggerTable.Get("name").String,
                    IsPreserved = triggerTable.Get("preserved").Type == DataType.Boolean
                        && triggerTable.Get("preserved").Boolean
                };
 
                trigger.Conditions.AddRange(parseConditions(triggerTable.Get("conditions").Table));
                trigger.Actions.AddRange(parseActions(triggerTable.Get("actions").Table));
 
                triggerList.Add(trigger);
            }
 
            return triggerList;
        }
 
        private List<ITriggerCondition> parseConditions(Table? conditions)
        {
            var conditionList = new List<ITriggerCondition>();
 
            if(conditions == null)
                return conditionList;
 
            foreach(var pairing in conditions.Pairs)
            {
                var condition = pairing.Value.ToObject<ITriggerCondition>();
                if(condition != null)
                    conditionList.Add(condition);
            }
 
            return conditionList;
        }
 
        private List<ITriggerAction> parseActions(Table? actions)
        {
            var actionList = new List<ITriggerAction>();
 
            if(actions == null)
                return actionList;
 
            foreach(var pairing in actions.Pairs)
            {
                var action = pairing.Value.ToObject<ITriggerAction>();
                if(action != null)
                    actionList.Add(action);
            }
 
            return actionList;
        }


        private Rectangle<int> parseRectangle(Table rect)
        {
            return new Rectangle<int>
            {
                X = (int)rect.Get("x").Number,
                Y = (int)rect.Get("y").Number,
                Width = (int)rect.Get("w").Number,
                Height = (int)rect.Get("h").Number,
            };
        }

        private Tile[][] parseTiles(DynValue tilesData, int width, int height)
        {
            var tiles = new Tile[width][];
            for (int x = 0; x < width; x++)
            {
                tiles[x] = new Tile[height];
                for (int y = 0; y < height; y++)
                {
                    tiles[x][y] = new Tile();
                }
            }

            if (tilesData.Type == DataType.Table)
            {
                var tileTable = tilesData.Table;
                
                foreach (var pair in tileTable.Pairs)
                {
                    try
                    {
                        string key = pair.Key.String;
                        string[] coords = key.Split(',');
                        
                        if (coords.Length != 2) continue;
                        if (!int.TryParse(coords[0], out int x)) continue;
                        if (!int.TryParse(coords[1], out int y)) continue;
                        
                        if (x < 0 || x >= width || y < 0 || y >= height) continue;

                        var tileEntry = pair.Value.Table;
                        if (tileEntry == null) continue;

                        tiles[x][y] = new Tile
                        {
                            TextureId = "dirt"
                        };
                    }
                    catch (System.Exception e)
                    {
                        Log.Error($"Error parsing tile: {e.Message}");
                    }
                }
            }

            return tiles;
        }
    }
}