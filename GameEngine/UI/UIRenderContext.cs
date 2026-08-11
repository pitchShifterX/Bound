using GameEngine.Graphics.Primitives;
using GameEngine.Graphics.Rendering;
using GameEngine.Resources;
using GameEngine.Utilities;
using GameEngine.Utilities.Extensions;

namespace GameEngine.UI
{
    public class UIRenderContext
    {
        private readonly IRenderContext _engineRenderContext;
        public readonly float Scale;
        public readonly float OffsetX;
        public readonly float OffsetY;

        public IntPtr Renderer => _engineRenderContext.Renderer;

        public UIRenderContext(
            IRenderContext engineRenderContext,
            float scale,
            float offsetX,
            float offsetY
        )
        {
            _engineRenderContext = engineRenderContext;

            Scale = scale;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }

        public Vector2<float> UIToScreenPosition(float x, float y)
        {
            var vector = new Vector2<float>(x, y);

            return UIToScreenPosition(vector);
        }

        public Vector2<float> UIToScreenPosition(Vector2<float> position)
        {
            return new Vector2<float>(
                position.x * Scale + OffsetX,
                position.y * Scale + OffsetY
            );
        }

        public Rectangle<float> UIToScreenRectangle(Rectangle<float> rectangle)
        {
            var position = UIToScreenPosition(rectangle.X, rectangle.Y);

            return new Rectangle<float>(
                position.x,
                position.y,
                ScaleValue(rectangle.Width),
                ScaleValue(rectangle.Height)
            );
        }

        public Vector2<float> UIToScreenSize(float x, float y)
        {
            return new Vector2<float>(
                Size(x),
                Size(y)
            );
        }

        public Vector2<float> UIToScreenSize(Vector2<float> size)
        {
            return new Vector2<float>(
                Size(size.x),
                Size(size.y)
            );

        }

        public Vector2<float> UIToScreenSize(Vector2<int> size)
        {
            return new Vector2<float>(
                Size(size.x),
                Size(size.y)
            );
        }

        public Vector2<float> ScreenToUI(float x, float y)
        {
            var vector = new Vector2<float>(x, y);

            return ScreenToUI(vector);
        }

        public Vector2<float> ScreenToUI(Vector2<float> position)
        {
            return new Vector2<float>(
                (position.x - OffsetX) / Scale,
                (position.y - OffsetY) / Scale
            );
        }

        public float ScaleValue(float value)
        {
            return value * Scale;
        }

        public float XPosition(float x)
            => x * Scale + OffsetX;

        public float YPosition(float y)
            => y * Scale + OffsetY;

        public float Size(float size)
            => size * Scale;

        public void DrawText(Font font, string label, Color color, Vector2<float> position)
        {
            var screenPosition = UIToScreenPosition(position);
            var textSize = font.CalculateSize(label);
            var screenSize = UIToScreenSize(textSize);

            var rect = new Rectangle<float>(
                screenPosition.x,
                screenPosition.y,
                screenSize.x,
                screenSize.y
            );

            _engineRenderContext.DrawText(font, label, color, rect.ToSDLRect());
        }

        public void DrawTexture(Texture texture, Rectangle<float>? source, Rectangle<float> destination)
        {
            var screenDestination = UIToScreenRectangle(destination);
            var destinationSDLRect = screenDestination.ToSDLRect();

            if (source.HasValue)
            {
                var sourceSDLRect = source.Value.ToSDLRect();

                _engineRenderContext.DrawTexture(
                    texture,
                    sourceSDLRect,
                    destinationSDLRect
                );
            }
            else
            {
                _engineRenderContext.DrawTexture(
                    texture,
                    null,
                    destinationSDLRect
                );
            }
        }

        public void DrawRectangle(Rectangle<float> rectangle, Color? color = null, Color? border = null)
        {
            var position = UIToScreenRectangle(rectangle);

            _engineRenderContext.DrawRectangle(position, color, border);
        }
    }
}