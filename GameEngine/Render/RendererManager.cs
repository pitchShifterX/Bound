using SDL2;
using GameEngine.Utilities;

namespace GameEngine.Render
{
    public class RendererManager(IRenderSettings settings, IntPtr window) : IRendererController
    {
        private IRenderSettings _settings = settings;
        private IntPtr _window = window;
        public IntPtr Renderer { get; private set; }

        public void Create()
        {
            var flags = SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED;
            if (_settings.VerticalSync)
            {
                flags |= SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC;
            }

            Renderer = SDL.SDL_CreateRenderer(_window, -1, flags);
        }

        public void SetVsync(bool value)
        {
            var isSuccess = SDL.SDL_RenderSetVSync(Renderer, value ? 1 : 0);

            if (isSuccess != 0)
            {
                Log.Error("Vertical synchronization failed to set");
            }
        }

        public void Draw(IntPtr texture, SDL.SDL_Rect source, SDL.SDL_Rect destination)
        {
            SDL.SDL_RenderCopy(Renderer, texture, ref source, ref destination);
        }

        public void Clear()
        {
            SDL.SDL_RenderClear(Renderer);
        }

        public void Destroy()
        {
            SDL.SDL_DestroyRenderer(Renderer);
        }
    }
}