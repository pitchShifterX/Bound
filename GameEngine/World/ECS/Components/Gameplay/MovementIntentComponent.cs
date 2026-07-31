using GameEngine.Utilities;

namespace GameEngine.World.ECS.Components
{
    public struct MovementIntentComponent
    {
        public Vector2<float> Direction;

        public MovementIntentComponent(Vector2<float>? direction = null)
        {
            Direction = direction ?? Vector2<float>.Zero;
        }
    }
}