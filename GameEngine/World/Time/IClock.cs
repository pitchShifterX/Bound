using GameEngine.SharedInterface;

namespace GameEngine.World.Time
{
    public interface IClock : IUpdatable
    {
        public float ElapsedSeconds { get; }
        public float DeltaSeconds { get; }
    }
}