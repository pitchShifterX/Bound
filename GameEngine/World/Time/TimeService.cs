using GameEngine.SharedInterface;

namespace GameEngine.World.Time
{
    public class TimeService : IInitializable
    {
        public IClock World { get; init; }

        public TimeService()
        {
            World = new WorldClock();
        }

        public void Initialize()
        {
            World.Initialize();
        }
    }
}