using GameEngine.Utilities;

namespace GameEngine.World.ECS.Components.Spatial
{
    [Flags]
    public enum CollisionLayer
    {
        None = 0,
        GroundUnit = 1 << 0,
        FlyingUnit = 1 << 1,
        Item = 1 << 2,
        Explosion = 1 << 3
    }

    public struct CollisionComponent
    {
        /// <summary>
        /// The current layer the entity exists.
        /// </summary>
        public CollisionLayer Layer;

        /// <summary>
        /// The layers where the entity can collide.
        /// </summary>
        public CollisionLayer Mask;
        
        /// <summary>
        /// Optional offset, not required to be set, 
        /// its purpose is similar to Bounds2DComponent.
        /// </summary>
        public Vector2<float> Offset;
    }
}