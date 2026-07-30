using GameEngine.Utilities;

namespace GameEngine.World.ECS.Components.Graphics
{
    public struct SpriteComponent
    {
        public string TextureId;
        public Rectangle<int> SourceRectangle;
        public Vector2<float> Size;
        public Vector2<float> Origin => new(
            Size.x / 2,
            Size.y / 2
        );
    }
}