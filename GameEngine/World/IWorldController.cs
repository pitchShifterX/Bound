using GameEngine.SharedInterface;

namespace GameEngine.World
{
    public interface IWorldController : ILoadable, IUpdatable
    {
        public void AddSystem(IWorldSystem system);
    }
}