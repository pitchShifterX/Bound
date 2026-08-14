using GameEngine.Event;
using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.Input;
using GameEngine.World.Input.Commands;
using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;
using GameEngine.World.Time;
using GameEngine.World.Unit;

namespace GameEngine
{
    public interface IGameplayContext : IUpdatable, IRenderable, IProcessInput
    {
        public ECSService ECS { get; }
        public TriggerEngine TriggerEngine { get; }
        public UnitService Unit { get; }
        public LocationService Location { get; }
        public PlayerService Player { get; }
        public CommandService Commands { get; }
        public TimeService Time { get; }
        
        public UIEventBus UIEvents { get; }

        public void LoadMap(string fileName);
    }
}