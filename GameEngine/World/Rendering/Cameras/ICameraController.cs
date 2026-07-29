using GameEngine.SharedInterface;

namespace GameEngine.World.Rendering.Cameras
{
    public interface ICameraController : IUpdatable
    {
        public float MovementSpeed { get; }

        public void SetViewportSize(int width, int height);
        public void SetZoom(float zoom);
        public void MoveDirection(Direction direction);
    }
}