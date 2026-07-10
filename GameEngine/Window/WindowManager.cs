using SDL2;
using GameEngine.Utilities;
using GameEngine.Exception;

namespace GameEngine.Window
{
    public class WindowManager : IWindowController
    {
        public IntPtr Window { get; private set; }

        private void CreateWindow(string title, int width, int height)
        {
            Window = SDL.SDL_CreateWindow(
                title,
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                width,
                height,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
            );

            if(Window == IntPtr.Zero)
            {
                throw new WindowCreationException(
                    SDL.SDL_GetError()
                );
            }
        }

        public void Create(string title, int width, int height)
            => CreateWindow(title, width, height);

        public void Create(string title, Vector2<int> resolution)
            => CreateWindow(title, resolution.x, resolution.y);

        public bool IsCreated()
            => Window != IntPtr.Zero;

        public void Destroy()
            => SDL.SDL_DestroyWindow(Window);
    }
}