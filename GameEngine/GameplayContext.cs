using GameEngine.Event.Input;
using GameEngine.Scene;
using GameEngine.World.ECS;
using GameEngine.World.Input;
using GameEngine.World.Map;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Rendering;
using GameEngine.World.Rendering.Cameras;
using GameEngine.World.Time;
using GameEngine.World.Unit;

namespace GameEngine
{
    public class GameplayContext : IGameplayContext
    {
        /// <summary>
        /// Context for underlying core systems which manage resources, 
        /// scenes, rendering, etc.
        /// </summary>
        private ISceneContext _sceneContext { get; init; }

        /// <summary>
        /// Registries for various pre-defined functionality. For example, 
        /// there are registries for conditions and actions to be used by 
        /// the trigger system. Mods can extend this list by adding to the 
        /// Registries property in GameplayManager.
        /// </summary>
        private GameRegistries _registries { get; init; }

        /// <summary>
        /// Manages the order of rendering.
        /// </summary>
        private RenderManager? _renderManager { get; set; }

        /// <summary>
        /// Reads map data from map context and initializes the entities, 
        /// ranging from units to locations.
        /// </summary>
        public MapInitializer? _mapInitializer { get; set; }

        /// <summary>
        /// Get the elapsed time since the map loaded.
        /// </summary>
        public IClock Time { get; set; }

        /// <summary>
        /// Core service for managing entities and components. This is often 
        /// passed around to services/systems for manipulating components on
        /// entities.
        /// </summary>
        public ECSService ECS { get; init; } = new();

        /// <summary>
        /// Context for map data. This is purely for reading data from the 
        /// map file.
        /// </summary>
        public IMapContext? MapContext { get; set; }

        /// <summary>
        /// Service for managing inputs (camera, ui, etc).
        /// </summary>
        public InputService? Input { get; set; }

        /// <summary>
        /// Context for controlling and viewing the camera.
        /// </summary>
        public CameraContext? Camera { get; set; }

        /// <summary>
        /// Engine for evaluating and executing triggers.
        /// </summary>
        public TriggerEngine TriggerEngine { get; init; }

        /// <summary>
        /// Service for managing the creation and destruction of units.
        /// </summary>
        public UnitService Unit { get; init; }

        /// <summary>
        /// Service for managing the creation and properties of locations. 
        /// Such properties could be hiding/displaying border color for 
        /// debugging or map making.
        /// </summary>
        public LocationService Location { get; init; }

        public GameplayContext(ISceneContext scene, GameRegistries registries)
        {
            _sceneContext = scene;
            _registries = registries;

            TriggerEngine = new TriggerEngine(this);
            Unit = new UnitService(ECS, _registries.UnitPrefab);
            Location = new LocationService(ECS);
            MapContext = new MapContext(_sceneContext.Paths.Maps, _registries.Triggers);

            _mapInitializer = new MapInitializer(this);

            Time = new WorldClock();
        }

        public void Load()
        {
            if(MapContext?.Data == null || MapContext.Data.Metadata == null)
                throw new System.Exception("Could not start game; map data missing!");

            _mapInitializer?.Initialize();

            Time = new WorldClock();
                
            var camera = new Camera(
                _sceneContext.Settings.WindowSize,
                MapContext.Data.Metadata.GetSize()!.Value
            );

            Camera = new CameraContext(camera);
            Input = new InputService(this);

            _renderManager = new RenderManager(
                MapContext,
                ECS,
                _sceneContext,
                Camera.View
            );
        }

        public void Unload(){}

        public void Process(IRecordInput input)
        {
            Input?.Process(input);
        }

        public void Update(float? delta)
        {
            Time.Update(delta);
            Camera?.Controller.Update(delta);
            TriggerEngine.Update(delta);
        }

        public void Render()
        {
            _renderManager?.Render();
        }
    }
}