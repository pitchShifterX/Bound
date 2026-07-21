using GameEngine.World.Map.Tiles;

namespace GameEngine.World.Map
{
    public class MapData
    {
        public MapMetadata? Metadata { get; set; }
        public Tile[][]? Tiles { get; set; }

        public override string ToString()
        {
            var meta = Metadata;

            return $"{meta?.Author} made {meta?.Title} with {meta?.Players?.Count} players.";
        }

        public Tile? GetTile(int x, int y)
        {
            if (Tiles == null || 
                x < 0 || y < 0 || 
                x >= Tiles.Length || y >= Tiles[0].Length
            )
                return null;

            return Tiles[x][y];
        }
    }
}