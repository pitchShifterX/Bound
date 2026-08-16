using GameEngine.Event;
using GameEngine.Event.Input;
using GameEngine.MapEditor.Bootstrap;
using GameEngine.Platform;
using GameEngine.Scene;
using GameEngine.UI.Event;
using GameEngine.World;
using GameEngine.World.Bootstrap;
using GameEngine.World.ECS;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;
using GameEngine.World.Rendering;
using GameEngine.World.Sounds;
using GameEngine.World.Time;
using GameEngine.World.Unit;

namespace GameEngine.MapEditor
{
    public class EditorContext : IWorldContext
    {
        /// <summary>
        /// Context for underlying core systems which manage resources, 
        /// scenes, rendering, etc.
        /// </summary>
        private ISceneContext _sceneContext { get; init; }

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
        /// Registries for various pre-defined functionality. For example, 
        /// there are registries for conditions and actions to be used by 
        /// the trigger system. Mods can extend this list by adding to the 
        /// Registries property in GameplayManager.
        /// </summary>
        public GameRegistries Registries => _sceneContext.Registries;

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
        /// Communication bridge to UI.
        /// </summary>
        public UIEventBus UIEvents => _sceneContext.UIEvents;

        public EditorContext(ISceneContext sceneContext, Random random)
        {
            _sceneContext = sceneContext;
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
        }

        public void LoadMap(string path)
        {
            // we expect a full path so let's just get the filename
            var fileName = Path.GetFileName(path);

            _bootstrap.LoadMap(fileName);
            _bootstrap.Validate();
            _bootstrap.Initialize();
        }

        public virtual void Process(IRecordInput input)
        {
            
        }

        public virtual void Update(float? delta)
        {
            if(Time != null && Time.Update(delta))
            {
                _sceneContext.UIEvents.Publish(new WorldSecondEvent(Time.WorldSeconds));
            }
        }

        public virtual void Render()
        {
            
        }
    }
}