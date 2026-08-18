using GameEngine.SharedInterface;
using GameEngine.Utilities;

namespace GameEngine.World.Rendering.Cameras
{
    public interface ICameraController : IUpdatable
    {
        public float MovementSpeed { get; }

        public void SetViewport(Rectangle<int> viewport);
        public void SetZoom(float zoom);
        public void MoveDirection(Direction direction);
    }
}