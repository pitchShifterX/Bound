using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.Utilities;
using GameEngine.World.Map;

namespace GameEngine.World.Assets
{
    public class AssetLoader
    {
        private ISceneContext _sceneContext;
        private GameRegistries _registries;

        public AssetLoader(
            ISceneContext sceneContext, 
            GameRegistries gameRegistries
        )
        {
            _sceneContext = sceneContext;
            _registries = gameRegistries;
        }

        public void Initialize(MapData map)
        {
            initializeTilesets(map);
        }

        private void initializeTilesets(MapData map)
        {
            var tilesetRegistry = _registries.Tilesets;
            var requestedTilesets = map.Tilesets;

            if(requestedTilesets == null) return;

            foreach(var requestedTileset in requestedTilesets)
            {
                Log.Info($"Loading {requestedTileset}");
                var tileset = tilesetRegistry.GetTilesetById(requestedTileset);

                _sceneContext.Load<Texture>(tileset.Id, tileset.TexturePath);
            }
        }
    }
}