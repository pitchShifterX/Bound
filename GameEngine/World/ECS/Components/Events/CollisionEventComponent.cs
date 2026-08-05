using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Spatial;

namespace GameEngine.World.ECS.Components.Events
{
    public struct CollisionEventComponent
    {
        public int Target;
        public CollisionLayer TargetLayer;
        public Vector2<float> Overlap;
    }
}