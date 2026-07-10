using GameEngine.SharedInterface;

namespace GameEngine.Mod
{
    public interface IMod : IInitializable, IUpdatable, IRenderable
    {
        public void Run();
    }
}