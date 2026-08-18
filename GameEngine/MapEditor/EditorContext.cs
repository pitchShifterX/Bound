using GameEngine.Event.Input;
using GameEngine.MapEditor.Bootstrap;
using GameEngine.MapEditor.Input;
using GameEngine.MapEditor.Runtime;
using GameEngine.MapEditor.Tools;
using GameEngine.Platform;
using GameEngine.Scene;
using GameEngine.UI.Elements.Editor;
using GameEngine.UI.Event;
using GameEngine.UI.Event.Types;
using GameEngine.Utilities;
using GameEngine.World.Bootstrap;
using GameEngine.World.ECS;
using GameEngine.World.Map;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;
using GameEngine.World.Rendering;
using GameEngine.World.Sounds;
using GameEngine.World.Time;
using GameEngine.World.Unit;

namespace GameEngine.MapEditor
{
    public class EditorContext : IEditorContext
    {
        /// <summary>
        /// Context for underlying core systems which manage resources, 
        /// scenes, rendering, etc.
        /// </summary>
        private ISceneContext _sceneContext { get; init; }

        /// <summary>
        /// The viewport to render to.
        /// </summary>
        private IEditorViewport _viewport { get; init; }

        /// <summary>
        /// Random instance for generating random data when necessary 
        /// e.g. random tiles on map.
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// Manages the order of rendering.
        /// </summary>
        private RenderManager? _renderManager { get; set; }

        /// <summary>
        /// Sets up our gameplay by loading and validating content.
        /// </summary>
        private IMapBootstrap _bootstrap;

        /// <summary>
        /// Updates and renders our editor.
        /// </summary>
        private EditorRuntime _runtime;

        /// <summary>
        /// Registries for various pre-defined functionality. For example, 
        /// there are registries for conditions and actions to be used by 
        /// the trigger system. Mods can extend this list by adding to the 
        /// Registries property in GameplayManager.
        /// </summary>
        public GameRegistries Registries => _sceneContext.Registries;

        /// <summary>
        /// The current map context.
        /// </summary>
        public IMapContext? Map { get; private set; }

        /// <summary>
        /// Service for opening and saving files.
        /// </summary>
        public FileService? File { get; private set; }

        /// <summary>
        /// Engine for evaluating and executing triggers.
        /// </summary>
        public TriggerEngine TriggerEngine { get; init; }

        /// <summary>
        /// Core service for managing entities and components. This is often 
        /// passed around to services/systems for manipulating components on
        /// entities.
        /// </summary>
        public ECSService ECS { get; init; } = new();

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
        /// Service utility for time management.
        /// </summary>
        public TimeService Time { get; init; }

        /// <summary>
        /// Service for controlling music and sounds.
        /// </summary>
        public SoundService Sound { get; init; }

        /// <summary>
        /// Abstract placement tool for tiles, units, etc.
        /// </summary>
        public PlacementTool? PlacementTool { get; set; }

        /// <summary>
        /// Communication bridge to UI.
        /// </summary>
        public UIEventBus UIEvents => _sceneContext.UIEvents;

        public EditorContext(ISceneContext sceneContext, IEditorViewport viewport, Random random)
        {
            _sceneContext = sceneContext;
            _viewport = viewport;
            _random = random;

            File = new FileService();
            Player = new PlayerService();
            TriggerEngine = new TriggerEngine(this);
            Location = new LocationService(ECS);
            Unit = new UnitService(ECS, Registries.UnitPrefab, Location);
            Time = new TimeService();
            Sound = new SoundService(_sceneContext, Registries.Sounds);

            _bootstrap = new EditorBootstrap(
                _sceneContext,
                Registries,
                ECS,
                Player,
                Location,
                TriggerEngine
            );

            _runtime = new EditorRuntime(
                _viewport,
                Registries,
                UIEvents,
                Player,
                TriggerEngine,
                Time,
                ECS
            );
        }

        public void LoadMap(string path)
        {
            // we expect a full path so let's just get the filename
            var fileName = Path.GetFileName(path);

            _bootstrap.LoadMap(fileName);
            _bootstrap.Validate();
            _bootstrap.Initialize();

            Map = _bootstrap.MapContext;

            _runtime.Initialize(
                _sceneContext,
                _bootstrap.MapContext,
                Player,
                ECS
            );

            initializeRendering();
        }

        public virtual void Process(IRecordInput input)
        {
            _runtime?.Input?.Process(input);

            if (_runtime?.Camera == null)
                return;

            var mousePosition = new Vector2<int>(input.MousePositionX, input.MousePositionY);

            if(!_runtime.Camera.View.Viewport.Contains(mousePosition))
                return;

            var worldPosition =
                _runtime.Camera.View.ScreenPositionToWorldPosition(mousePosition.To<float>());

            var tilePosition =
                Map?.TileCoordinateConverter?.WorldPositionToTile(worldPosition.x, worldPosition.y);

            if (tilePosition == null)
                return;

            var editorInput = new EditorInput(
                mousePosition,
                worldPosition.To<int>(),
                tilePosition.Value,
                input.WasMouseButtonPressed(MouseButton.Left),
                input.IsMouseButtonPressed(MouseButton.Left)
            );

            UIEvents.Publish(
                new TileHoverEvent(
                    tilePosition.Value.x, 
                    tilePosition.Value.y
                )
            );

            PlacementTool?.Process(this, editorInput);
        }

        public virtual void Update(float? delta)
        {
            _runtime?.Update(delta);
            
            if(Time != null && Time.Update(delta))
            {
                _sceneContext.UIEvents.Publish(new WorldSecondEvent(Time.WorldSeconds));
            }
        }

        public virtual void Render()
        {
            var bounds = _viewport.Bounds;

            _sceneContext.SetClipRect(bounds.To<int>());

            _renderManager?.Render();

            _sceneContext.SetClipRect(null);
        }

        private void initializeRendering()
        {
            _renderManager = new RenderManager(
                _bootstrap.MapContext,
                ECS,
                _sceneContext,
                _runtime.Camera!.View,
                _runtime.Selection!,
                Registries.Tilesets
            );
        }
    }
}