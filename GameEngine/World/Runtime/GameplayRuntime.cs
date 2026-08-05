using GameEngine.Scene;
using GameEngine.World.ECS;
using GameEngine.World.Input;
using GameEngine.World.Input.Commands;
using GameEngine.World.Map;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;
using GameEngine.World.Rendering.Cameras;
using GameEngine.World.Spatial;
using GameEngine.World.Time;

namespace GameEngine.World.Runtime
{
    public class GameplayRuntime : IGameplayRuntime
    {
        private readonly GameRegistries _registries;
        private readonly PlayerService _player;
        private readonly TriggerEngine _trigger;
        private readonly TimeService _time;
        private readonly ECSService _ecs;

        /// <summary>
        /// Manages multiple gameplay systems that process components.
        /// </summary>
        private readonly GameplaySystems _systems;

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
        /// Divvy up the map into a bigger grid for efficient 
        /// calculations.
        /// </summary>
        public SpatialHashGrid SpatialHashGrid { get; }

        public GameplayRuntime(
            GameRegistries registries, 
            PlayerService player, 
            TriggerEngine trigger,
            TimeService time,
            ECSService ecs
        )
        {
            _registries = registries;
            _player = player;
            _trigger = trigger;
            _time = time;
            _ecs = ecs;

            SpatialHashGrid = new SpatialHashGrid();

            _systems = new GameplaySystems(_ecs, SpatialHashGrid);
        }

        public void Initialize(
            ISceneContext scene, 
            IMapContext map, 
            PlayerService player, 
            CommandService commands,
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
            Input = new InputService(Selection, Camera, commands);
        }

        public void Update(float? delta)
        {
            _time.World.Update(delta);
            Camera?.Controller.Update(delta);
            _systems.Update(delta);
            _trigger.Update(delta);
        }
    }
}