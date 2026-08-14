using GameEngine.Event;
using GameEngine.Event.Input;
using GameEngine.Scene;
using GameEngine.World.Bootstrap;
using GameEngine.World.ECS;
using GameEngine.World.Input.Commands;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;
using GameEngine.World.Rendering;
using GameEngine.World.Runtime;
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
        /// Sets up our gameplay by loading and validating content.
        /// </summary>
        private IGameplayBootstrap _bootstrap;

        /// <summary>
        /// Initializes and updates gameplay systems.
        /// </summary>
        private IGameplayRuntime _runtime;

        /// <summary>
        /// Core service for managing entities and components. This is often 
        /// passed around to services/systems for manipulating components on
        /// entities.
        /// </summary>
        public ECSService ECS { get; init; } = new();

        /// <summary>
        /// Engine for evaluating and executing triggers.
        /// </summary>
        public TriggerEngine TriggerEngine { get; init; }

        /// <summary>
        /// Service for managing players (and computers).
        /// </summary>
        public PlayerService Player { get; set; }

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

        /// <summary>
        /// Service utility for time management.
        /// </summary>
        public TimeService Time { get; init; }

        /// <summary>
        /// Communication bridge to UI.
        /// </summary>
        public UIEventBus UIEvents => _sceneContext.UIEvents;

        public GameplayContext(ISceneContext scene, GameRegistries registries)
        {
            _sceneContext = scene;
            _registries = registries;

            Player = new PlayerService();
            TriggerEngine = new TriggerEngine(this);
            Location = new LocationService(ECS);
            Unit = new UnitService(ECS, _registries.UnitPrefab, Location);
            Commands = new CommandService(ECS);
            Time = new TimeService();

            _bootstrap = new GameplayBootstrap(
                _sceneContext,
                _registries,
                ECS,
                Player,
                Location,
                TriggerEngine
            );

            _runtime = new GameplayRuntime(
                _registries,
                UIEvents,
                Player,
                TriggerEngine,
                Time,
                ECS
            );
        }

        public void LoadMap(string fileName)
        {
            _bootstrap.LoadMap(fileName);
            _bootstrap.Validate();
            _bootstrap.Initialize();

            _runtime.Initialize(
                _sceneContext,
                _bootstrap.MapContext,
                Player,
                Commands,
                ECS
            );

            initializeRendering();
        }

        public void Process(IRecordInput input)
        {
            _runtime?.Input?.Process(input);
        }

        public void Update(float? delta)
        {
            _runtime?.Update(delta);
        }

        public void Render()
        {
            _renderManager?.Render();
        }

        private void initializeRendering()
        {
            // should refactor
            _renderManager = new RenderManager(
                _bootstrap.MapContext,
                ECS,
                _sceneContext,
                _runtime.Camera!.View,
                _runtime.Selection!,
                _registries.Tilesets
            );
        }
    }
}