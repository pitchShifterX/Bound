using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;

namespace GameEngine.Graphics.Drawing
{
    public interface IDrawRectangle
    {
        public void DrawRectangle(Rectangle<float> rectangle, Color? color = null, Color? border = null);
    }
}