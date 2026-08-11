using SDL2;
using GameEngine.Utilities;
using GameEngine.Resources;
using GameEngine.Graphics.Primitives;

namespace GameEngine.Graphics.Rendering
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

            SDL.SDL_SetRenderDrawBlendMode(
                Renderer,
                SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND
            );
        }

        public void SetVsync(bool value)
        {
            var isSuccess = SDL.SDL_RenderSetVSync(Renderer, value ? 1 : 0);

            if (isSuccess != 0)
            {
                Log.Error("Vertical synchronization failed to set");
            }
        }

        public void DrawTexture(IntPtr texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination)
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

        public void DrawTexture(Texture texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination)
        {
            DrawTexture(texture.Handle, source, destination);
        }

        public void DrawEllipse(Vector2<int> center, float radiusX, float radiusY, Color color)
        {
            const int segments = 64;

            var points = new SDL.SDL_Point[segments + 1];

            for (int i = 0; i <= segments; i++)
            {
                float angle = MathF.Tau * i / segments;

                points[i] = new SDL.SDL_Point
                {
                    x = center.x + (int)(MathF.Cos(angle) * radiusX),
                    y = center.y + (int)(MathF.Sin(angle) * radiusY)
                };
            }

            SDL.SDL_SetRenderDrawColor(
                Renderer,
                color.R,
                color.G,
                color.B,
                color.A
            );

            SDL.SDL_RenderDrawLines(
                Renderer,
                points,
                points.Length
            );
        }

        public void DrawRectangle(Rectangle<float> rectangle, Color? color = null, Color? border = null)
        {
            var rect = new SDL.SDL_Rect
            {
                x = (int)rectangle.X,
                y = (int)rectangle.Y,
                w = (int)rectangle.Width,
                h = (int)rectangle.Height
            };

            if(color != null)
            {
                SDL.SDL_SetRenderDrawColor(Renderer, color.Value.R, color.Value.G, color.Value.B, color.Value.A);

                SDL.SDL_RenderFillRect(Renderer, ref rect);
            }
            
            if(border != null)
            {
                SDL.SDL_SetRenderDrawColor(Renderer, border.Value.R, border.Value.G, border.Value.B, border.Value.A);

                SDL.SDL_RenderDrawRect(Renderer, ref rect);
            }
        }

        public void DrawText(IntPtr textureHandle, ref SDL.SDL_Rect destination)
        {
            if (textureHandle == IntPtr.Zero) return;

            SDL.SDL_QueryTexture(textureHandle, out _, out _, out var width, out var height);
            destination.w = width;
            destination.h = height;

            SDL.SDL_RenderCopy(Renderer, textureHandle, IntPtr.Zero, ref destination);
        }

        public void Present()
        {
            SDL.SDL_SetRenderDrawColor(Renderer, 0, 0, 0, 255);

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