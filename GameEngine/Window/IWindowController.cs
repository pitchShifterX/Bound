using GameEngine.Utilities;

namespace GameEngine.Window
{
    public interface IWindowController
    {
        public IntPtr Window { get; }
        public void Create(string title, int width, int height);
        public void Create(string title, Vector2<int> resolution);
        public bool IsCreated();
        public void Destroy();
    }
}