using GameEngine.Utilities;

namespace GameEngine.World.Rendering.Cameras
{
    public interface ICameraView
    {
        public int ViewportWidth { get; }
        public int ViewportHeight { get; }
        public Vector2<float> WorldPosition { get; }

        public float Zoom { get; }
        
        public Rectangle<float> VisibleWorldBounds { get; }

        public bool IsVisible(Rectangle<float> bounds);

        public Vector2<float> ScreenPositionToWorldPosition(int screenX, int screenY);
        public Vector2<float> ScreenPositionToWorldPosition(Vector2<float> screenPosition);
        public Vector2<int> WorldPositionToScreenPosition(float worldX, float worldY);
        public Rectangle<float> WorldToViewportRectangle(Rectangle<float> worldPosition);
    }
}