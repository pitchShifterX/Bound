using GameEngine.SharedInterface;
using GameEngine.UI.Event;
using GameEngine.World.ECS;
using GameEngine.World.Input;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;
using GameEngine.World.Sounds;
using GameEngine.World.Time;
using GameEngine.World.Unit;

namespace GameEngine.World
{
    public interface IWorldContext : IUpdatable, IRenderable, IProcessInput
    {
        public ECSService ECS { get; }
        public TriggerEngine TriggerEngine { get; }
        public UnitService Unit { get; }
        public LocationService Location { get; }
        public PlayerService Player { get; }
        public TimeService Time { get; }
        public SoundService Sound { get; }
        
        public UIEventBus UIEvents { get; }

        public void LoadMap(string fileName);
    }
}