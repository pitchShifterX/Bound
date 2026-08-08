using GameEngine.Graphics.Drawing;
using GameEngine.Resources;
using SDL2;

namespace GameEngine.Graphics.Rendering
{
    public interface IRenderContext : IDrawPrimitive, IResourceProvider
    {
        public IntPtr Renderer { get; }

        public void DrawText(Font font, string text, SDL.SDL_Color color, SDL.SDL_Rect destination);
    }
}