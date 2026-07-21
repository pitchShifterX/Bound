using GameEngine.Exception;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Player;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map
{
    public class MapLuaLoader
    {
        private Script? _mapScript;

        public MapData Load(string path)
        {
            if(!File.Exists(path))
                throw new MapNotFoundException($"Map not found: {path}");
            
            _mapScript = new Script();
            var dataChunk = _mapScript.LoadFile(path);
            _mapScript.Call(dataChunk);

            var mainFunction = _mapScript.Globals.Get("main");
            if(mainFunction.Type != DataType.Function)
                throw new InvalidOperationException($"Map missing main()");
            
            var mapData = _mapScript.Call(mainFunction);

            return parseMapData(mapData.Table);
        }

        private MapData parseMapData(Table mapData)
        {
            var metadata = parseMetadata(mapData.Get("metadata").Table);
            var tiles = parseTiles(mapData.Get("tiles"), metadata.Width.Value, metadata.Height.Value);

            return new MapData
            {
                Metadata = metadata,
                Tiles = tiles
            };
        }

        private MapMetadata parseMetadata(Table metadata)
        {
            var players = parsePlayers(metadata.Get("players").Table);

            return new MapMetadata
            {
                Title = metadata.Get("title").String,
                Description = metadata.Get("description").String,
                Author = metadata.Get("author").String,
                Width = (int)metadata.Get("width").Number,
                Height = (int)metadata.Get("height").Number,
                Players = players
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
                    Id = pairing.Key.String,
                    Color = playerTable.Get("color").String,
                    IsHuman = playerTable.Get("human").Boolean
                });
            }

            return playerList;
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
                        Console.WriteLine($"Error parsing tile: {e.Message}");
                    }
                }
            }

            return tiles;
        }
    }
}