using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;

namespace GameEngine.World.ECS.Components.Graphics
{
    public struct SelectionCircleComponent
    {
        public float Radius;
        public Color Color;
        public Vector2<float> Offset;
    }
}