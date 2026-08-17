using GameEngine.Scene;
using GameEngine.UI.Elements.Editor;
using GameEngine.UI.Event;
using GameEngine.World.ECS;
using GameEngine.World.Input;
using GameEngine.World.Map;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;
using GameEngine.World.Rendering.Cameras;
using GameEngine.World.Runtime;
using GameEngine.World.Spatial;
using GameEngine.World.Time;

namespace GameEngine.MapEditor.Runtime
{
    public class EditorRuntime : IWorldRuntime
    {
        // need to fix a small viewport issue where top and bottom rows 
        // of tiles not rendering completely
        private readonly IEditorViewport _viewport;
        private readonly GameRegistries _registries;
        private readonly PlayerService _player;
        private readonly TriggerEngine _trigger;
        private readonly TimeService _time;
        private readonly ECSService _ecs;

        /// <summary>
        /// Manages multiple gameplay systems that process components.
        /// </summary>
        private GameplaySystems? _systems;

        /// <summary>
        /// Context for controlling and viewing the camera.
        /// </summary>
        public CameraContext? Camera { get; private set; }

        /// <summary>
        /// Service for selecting units.
        /// </summary>
        public SelectionService? Selection { get; private set; }

        /// <summary>
        /// Service for managing inputs (camera, ui, etc).
        /// </summary>
        public InputService? Input { get; private set; }

        /// <summary>
        /// Service for getting tile properties.
        /// </summary>
        public TerrainService? Terrain { get; private set; }

        /// <summary>
        /// Divvy up the map into a bigger grid for efficient 
        /// calculations.
        /// </summary>
        public SpatialHashGrid SpatialHashGrid { get; }

        /// <summary>
        /// Communication bridge from gameplay and UI.
        /// </summary>
        public UIEventBus UIEvents { get; }

        public EditorRuntime(
            IEditorViewport viewport,
            GameRegistries registries,
            UIEventBus uiEvents,
            PlayerService player, 
            TriggerEngine trigger,
            TimeService time,
            ECSService ecs
        )
        {
            _viewport = viewport;
            _registries = registries;
            _player = player;
            _trigger = trigger;
            _time = time;
            _ecs = ecs;

            SpatialHashGrid = new SpatialHashGrid();
            UIEvents = uiEvents;
        }

        public void Initialize(
            ISceneContext scene, 
            IMapContext map, 
            PlayerService player, 
            ECSService ecs
        )
        {
            _time.Initialize();

            var camera = new Camera(
                scene.Settings.WindowSize,
                map.Data!.Metadata!.GetSize()!.Value
            );

            Camera = new CameraContext(camera);
            Selection = new SelectionService(player, ecs, camera);
            Input = new InputService(Selection, Camera);
            Terrain = new TerrainService(map, _registries.Tilesets);

            _systems = new GameplaySystems(_ecs, SpatialHashGrid, Terrain);
        }

        public void Update(float? delta)
        {
            // fires a UI event every second for any UI elements listening
            if(_time.Update(delta))
            {
                UIEvents.Publish(new WorldSecondEvent(_time.WorldSeconds));
            }

            Camera?.Controller.Update(delta);
            _systems?.Update(delta);
            _trigger.Update(delta);
        }
    }
}