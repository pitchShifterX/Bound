using GameEngine.Resources;

namespace GameEngine.Graphics
{
    public class TextData
    {
        public Font Font { get; init; }
        public Color Color { get; init; }

        public TextData(Font font, Color color)
        {
            Font = font;
            Color = color;
        }
    }
}