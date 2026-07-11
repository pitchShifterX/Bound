using GameEngine.SharedInterface;

namespace GameEngine.Mod
{
    public interface IMod : IInitializable, IUpdatable, IRenderable
    {
        public void Start();
        public void Run();
        public void Close();
    }
}