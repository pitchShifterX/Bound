using SDL2;

namespace GameEngine.Render
{
    public interface IRendererController
    {
        public void Create();
        public void Draw(IntPtr texture, SDL.SDL_Rect source, SDL.SDL_Rect destination);
        public void Clear();
        public void Destroy();
    }
}