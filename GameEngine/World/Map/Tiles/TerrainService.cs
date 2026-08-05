using GameEngine.Utilities;

namespace GameEngine.World.Map.Tiles
{
    public class TerrainService
    {
        private readonly IMapContext _map;
        private readonly TilesetRegistry _tilesetRegistry;

        public TerrainService(IMapContext map, TilesetRegistry registry)
        {
            _map = map;
            _tilesetRegistry = registry;
        }

        public bool IsWalkable(Rectangle<float> bounds)
        {
            foreach(var tile in _map.GetTiles(bounds))
            {
                var tileset = _tilesetRegistry.GetTilesetById(tile.TilesetId);

                if(tile.TileIndex < 0 || tile.TileIndex >= tileset.TileDefinitions.Length)
                {
                    Log.Error(
                        $"Tile index {tile.TileIndex} is invalid for tileset {tile.TilesetId}. " +
                        $"Definitions: {tileset.TileDefinitions.Length}"
                    );

                    return false;
                }

                var definition = tileset.TileDefinitions[tile.TileIndex];

                if(!definition.IsWalkable)
                    return false;
            }

            return true;
        }
    }
}