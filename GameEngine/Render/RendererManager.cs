using SDL2;
using GameEngine.Utilities;
using GameEngine.Resources;

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

        public void Draw(IntPtr texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination)
        {
            if(texture == IntPtr.Zero) return;

            if(source.HasValue)
            {
                var src = source.Value;

                SDL.SDL_RenderCopy(Renderer, texture, ref src, ref destination);
            }
            else
            {
                SDL.SDL_RenderCopy(Renderer, texture, IntPtr.Zero, ref destination);
            }
        }

        public void DrawDynamicText(Font font, string text, SDL.SDL_Color color, SDL.SDL_Rect destination)
        {
            if(font.Handle == IntPtr.Zero) return;

            var surface = SDL_ttf.TTF_RenderUTF8_Blended(font.Handle, text, color);
            if (surface == IntPtr.Zero) return;

            try
            {
                var texture = SDL.SDL_CreateTextureFromSurface(Renderer, surface);
                if (texture == IntPtr.Zero) return;

                var queryTexture = SDL.SDL_QueryTexture(
                    texture,
                    out _,
                    out _,
                    out var width,
                    out var height
                );

                destination.w = width;
                destination.h = height;

                try
                {
                    Draw(texture, null, destination);
                }
                finally
                {
                    SDL.SDL_DestroyTexture(texture);
                }
            }
            finally
            {
                SDL.SDL_FreeSurface(surface);
            }
        }

        public void Present()
        {
            SDL.SDL_RenderPresent(Renderer);
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