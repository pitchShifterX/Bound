using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;

namespace GameEngine.Graphics.Drawing
{
    public interface IDrawEllipse
    {
        public void DrawEllipse(Vector2<int> center, float radiusX, float radiusY, Color color);
    }
}