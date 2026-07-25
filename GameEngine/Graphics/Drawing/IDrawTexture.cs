using GameEngine.Resources;
using SDL2;

namespace GameEngine.Graphics.Drawing
{
    public interface IDrawTexture
    {
        public void DrawTexture(IntPtr texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination);
        public void DrawTexture(Texture texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination);
    }
}