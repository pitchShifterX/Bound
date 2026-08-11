using GameEngine.Graphics.Drawing;
using GameEngine.Resources;
using SDL2;

namespace GameEngine.Graphics.Rendering
{
    public interface IRendererController : IDrawPrimitive
    {
        public IntPtr Renderer { get; }
        public void Create();
        public void DrawText(IntPtr textureHandle, ref SDL.SDL_Rect destination);
        public void Present();
        public void Clear();
        public void Destroy();
    }
}