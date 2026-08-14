using GameEngine.SharedInterface;

namespace GameEngine.World.Time
{
    public class TimeService : IInitializable
    {
        private int _lastWorldSecond;

        public IClock World { get; init; }

        public int WorldSeconds => (int)World.ElapsedSeconds;

        public TimeService()
        {
            World = new WorldClock();
        }

        public void Initialize()
        {
            World.Initialize();

            _lastWorldSecond = 0;
        }

        public bool Update(float? delta)
        {
            World.Update(delta);

            var currentSecond = (int)World.ElapsedSeconds;

            if(currentSecond == _lastWorldSecond)
                return false;

            _lastWorldSecond = currentSecond;

            return true;
        }
    }
}