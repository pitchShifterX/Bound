using GameEngine.Event.Input;
using GameEngine.Scene;
using GameEngine.World.Assets;
using GameEngine.World.ECS;
using GameEngine.World.Input;
using GameEngine.World.Input.Commands;
using GameEngine.World.Map;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;
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
        public GameRegistries _registries { get; init; }

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
        public IClock? Time { get; set; }

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
        /// Loads assets requested by the map. Pre-defined tilesets are 
        /// registered in GameplayManager -> GameRegistries. Similarly, 
        /// unit prefabs will have their textures loaded if the map uses 
        /// the unit.
        /// </summary>
        public AssetLoader AssetLoader { get; set; }

        /// <summary>
        /// Manages multiple gameplay systems that process components.
        /// </summary>
        public GameplaySystems GameplaySystems { get; set; }

        /// <summary>
        /// Service for managing inputs (camera, ui, etc).
        /// </summary>
        public InputService? Input { get; set; }

        /// <summary>
        /// Service for selecting units.
        /// </summary>
        public SelectionService? Selection { get; set; }

        /// <summary>
        /// Context for controlling and viewing the camera.
        /// </summary>
        public CameraContext? Camera { get; set; }

        /// <summary>
        /// Engine for evaluating and executing triggers.
        /// </summary>
        public TriggerEngine TriggerEngine { get; init; }

        /// <summary>
        /// Service for managing players (and computers).
        /// </summary>
        public PlayerService? Player { get; set; }

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

        /// <summary>
        /// Commands are mouse-issued orders. Gamepad will not use this.
        /// </summary>
        public CommandService Commands { get; init; }

        public GameplayContext(ISceneContext scene, GameRegistries registries)
        {
            _sceneContext = scene;
            _registries = registries;

            Player = new PlayerService();
            TriggerEngine = new TriggerEngine(this);
            Location = new LocationService(ECS);
            GameplaySystems = new GameplaySystems(ECS);
            Unit = new UnitService(ECS, _registries.UnitPrefab, Location);
            MapContext = new MapContext(_sceneContext.Paths.Maps, _registries.Triggers);
            AssetLoader = new AssetLoader(_sceneContext, _registries);

            Commands = new CommandService(ECS);

            _mapInitializer = new MapInitializer(this);
        }

        public void Load()
        {
            validate();

            initializeMap();

            initializeRuntime();

            initializeRendering();
        }

        public void Unload(){}

        public void Process(IRecordInput input)
        {
            Input?.Process(input);
        }

        public void Update(float? delta)
        {
            Time?.Update(delta);
            Camera?.Controller.Update(delta);
            GameplaySystems.Update(delta);
            TriggerEngine.Update(delta);
        }

        public void Render()
        {
            _renderManager?.Render();
        }

        private void validate()
        {
            if(MapContext?.Data == null || MapContext.Data.Metadata == null)
                throw new System.Exception("Could not start game; map data missing!");
            
            if(Player == null)
                throw new System.Exception("Player service null. Check map for issues.");
        }

        private void initializeMap()
        {
            if(MapContext?.Data == null)
                throw new System.Exception("Could not initialize map.");

            AssetLoader.Initialize(MapContext.Data);
            
            _mapInitializer?.Initialize();
        }

        private void initializeRuntime()
        {
            Time = new WorldClock();
                
            var camera = new Camera(
                _sceneContext.Settings.WindowSize,
                MapContext!.Data!.Metadata!.GetSize()!.Value
            );

            Camera = new CameraContext(camera);
            Selection = new SelectionService(Player!, ECS, camera);
            Input = new InputService(this, Camera.Controller);
        }

        private void initializeRendering()
        {
            // should refactor
            _renderManager = new RenderManager(
                MapContext!,
                ECS,
                _sceneContext,
                Camera!.View,
                Selection!,
                _registries.Tilesets
            );
        }
    }
}