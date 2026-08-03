using GameEngine.Utilities;
using GameEngine.World.Map.Tiles;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua.Tiles
{
    public class LuaTileParser
    {
        public Tile[][] Parse(Table table, int mapWidth, int mapHeight)
        {
            var tiles = new Tile[mapWidth][];

            for(int x = 0; x < mapWidth; x++)
                tiles[x] = new Tile[mapHeight];

            var lua = new LuaTableReader(table);

            foreach(var pair in lua.Pairs())
            {
                try
                {
                    string key = pair.Key.String;
                    string[] coords = key.Split(",");

                    if (coords.Length != 2) continue;
                    if (!int.TryParse(coords[0], out int x)) continue;
                    if (!int.TryParse(coords[1], out int y)) continue;
                    
                    if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight) continue;

                    var tileEntry = pair.Value.Table;
                    if (tileEntry == null) continue;

                    var tilesetValue = tileEntry.Get("tileset");
                    var tileIndexValue = tileEntry.Get("tileIndex");

                    tiles[x][y] = new Tile
                    {
                        TilesetId = tilesetValue.String,
                        TileIndex = (int)tileIndexValue.Number
                    };
                }
                catch(System.Exception e)
                {
                    Log.Error($"Error parsing tile: {e.Message}");

                    throw;
                }
            }

            return tiles;
        }
    }
}