using GameEngine.Resources;
using GameEngine.Scene;
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
            initializeUnitPrefabs();
            initializeTilesets(map);
            initializeSounds(map);
        }
        
        private void initializeUnitPrefabs()
        {
            foreach(var prefab in _registries.UnitPrefab.Prefabs)
            {
                var unit = prefab.Value;

                _sceneContext.Load<Texture>(unit.Name, unit.TexturePath);
            }
        }

        private void initializeTilesets(MapData map)
        {
            var tilesetRegistry = _registries.Tilesets;
            var requestedTilesets = map.Tilesets;

            if(requestedTilesets == null) return;

            foreach(var requestedTileset in requestedTilesets)
            {
                var tileset = tilesetRegistry.GetTilesetById(requestedTileset);

                _sceneContext.Load<Texture>(tileset.Id, tileset.TexturePath);
            }
        }

        private void initializeSounds(MapData map)
        {
            var soundRegistry = _registries.Sounds;
            var requestedSounds = map.Sounds;

            if(requestedSounds == null) return;

            foreach(var requestedSound in requestedSounds)
            {
                if(soundRegistry.Get(requestedSound, out var sound))
                {
                    _sceneContext.Load<Resources.Audio>(sound.Id, sound.Path);
                }
            }
        }
    }
}