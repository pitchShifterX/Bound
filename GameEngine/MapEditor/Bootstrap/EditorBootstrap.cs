using GameEngine.Scene;
using GameEngine.Utilities;
using GameEngine.World.Assets;
using GameEngine.World.Bootstrap;
using GameEngine.World.ECS;
using GameEngine.World.Map;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;

namespace GameEngine.MapEditor.Bootstrap
{
    public class EditorBootstrap : IMapBootstrap
    {
        private readonly ISceneContext _sceneContext;
        private readonly GameRegistries _registries;
        private readonly ECSService _ecs;
        
        /// <summary>
        /// Reads map data from map context and initializes the entities, 
        /// ranging from units to locations.
        /// </summary>
        public MapInitializer MapInitializer { get; private set; }

        /// <summary>
        /// Context for map data. This is purely for reading data from the 
        /// map file.
        /// </summary>
        public IMapContext MapContext { get; private set; }

        /// <summary>
        /// Loads assets requested by the map. Pre-defined tilesets are 
        /// registered in GameplayManager -> GameRegistries. Similarly, 
        /// unit prefabs will have their textures loaded if the map uses 
        /// the unit.
        /// </summary>
        public AssetLoader AssetLoader { get; private set; }

        public EditorBootstrap(
            ISceneContext sceneContext, 
            GameRegistries registries,
            ECSService ecs,
            PlayerService players,
            LocationService locations,
            TriggerEngine triggers
        )
        {
            _sceneContext = sceneContext;
            _registries = registries;
            _ecs = ecs;

            MapContext = new MapContext(_sceneContext.Paths.Maps, _registries.Triggers);
            AssetLoader = new AssetLoader(_sceneContext, _registries);

            MapInitializer = new MapInitializer(
                players,
                locations,
                triggers
            );
        }

        public void Validate()
        {
            if(MapContext?.Data == null || MapContext.Data.Metadata == null)
                throw new System.Exception("Could not initialize world; map data missing!");
        }

        public void LoadMap(string fileName)
        {
            Log.Info("called");
            MapContext.LoadMap(fileName);

            Log.Info(MapContext.Data!.ToString());
        }

        public void Initialize()
        {
            AssetLoader.Initialize(MapContext.Data!);
        }
    }
}