using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.Input;
using GameEngine.World.Map;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Rendering.Cameras;
using GameEngine.World.Time;
using GameEngine.World.Unit;

namespace GameEngine
{
    public interface IGameplayContext : ILoadable, IUpdatable, IRenderable, IProcessInput
    {
        public IClock Time { get; }
        public ECSService ECS { get; }
        public IMapContext? MapContext { get; }
        public CameraContext? Camera { get; }
        public TriggerEngine TriggerEngine { get; }
        public UnitService Unit { get; }
        public LocationService Location { get; }
        public SelectionService? Selection { get; }
    }
}