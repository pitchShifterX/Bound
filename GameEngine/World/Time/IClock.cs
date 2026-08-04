using GameEngine.SharedInterface;

namespace GameEngine.World.Time
{
    public interface IClock : IInitializable, IUpdatable
    {
        public float ElapsedSeconds { get; }
        public float DeltaSeconds { get; }
    }
}