using System.Numerics;
using GameEngine.Utilities;

namespace GameEngine.Render
{
    public interface ICameraView
    {
        public int ViewportWidth { get; }
        public int ViewportHeight { get; }
        public Vector2 WorldPosition { get; }

        public float Zoom { get; }
        
        public Rectangle<float> VisibleWorldBounds { get; }

        public Vector2 ScreenPositionToWorldPosition(Vector2 screenPosition);
        public Vector2<int> WorldPositionToScreenPosition(float worldX, float worldY);
    }
}