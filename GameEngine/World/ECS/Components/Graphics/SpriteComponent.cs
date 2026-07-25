using GameEngine.Utilities;

namespace GameEngine.World.ECS.Components.Graphics
{
    public struct SpriteComponent
    {
        public string TextureId;
        public Rectangle<int> SourceRectangle;
    }
}