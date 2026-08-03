using GameEngine.Utilities;

namespace GameEngine.World.Map.Tiles
{
    public class TilesetRegistry
    {
        private readonly Dictionary<string, TilesetDefinition> _tiles = [];

        public IReadOnlyDictionary<string, TilesetDefinition>
            Tiles => _tiles;
        
        public void Register(TilesetDefinition tile)
        {
            try
            {
                _tiles.Add(tile.Id, tile);
            }
            catch(System.Exception e)
            {
                Log.Error($"Could not register tileset: {e}");

                throw;
            }
        }

        public TilesetDefinition GetTilesetById(string id)
        {
            if(!_tiles.TryGetValue(id, out var tileset))
            {
                throw new NullReferenceException($"Tileset with id {id} not registered.");
            }

            return tileset;
        }
    }
}