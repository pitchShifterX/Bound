using GameEngine.SharedInterface;

namespace GameEngine.Mod
{
    public interface IMod : IInitializable
    {
        public void Start();
        public void Run();
        public void Close();
    }
}