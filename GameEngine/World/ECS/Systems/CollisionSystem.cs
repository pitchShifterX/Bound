using GameEngine.SharedInterface;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Events;
using GameEngine.World.ECS.Components.Spatial;
using GameEngine.World.Spatial;

namespace GameEngine.World.ECS.Systems
{
    public class CollisionSystem : IUpdatable
    {
        private readonly ECSService _ecs;
        private readonly SpatialHashGrid _grid;

        public CollisionSystem(
            ECSService ecs,
            SpatialHashGrid grid
        )
        {
            _ecs = ecs;
            _grid = grid;
        }

        public void Update(float? delta)
        {
            foreach (var entity in _ecs.GetEntitiesWith<CollisionEventComponent>())
            {
                _ecs.RemoveComponent<CollisionEventComponent>(entity);
            }
            
            var entities = _ecs.GetEntitiesWith<CollisionComponent, Bounds2DComponent>();

            foreach(var entity in entities)
            {
                ref var collision = ref _ecs.GetComponent<CollisionComponent>(entity);
                ref var bounds = ref _ecs.GetComponent<Bounds2DComponent>(entity);

                var worldBounds = getWorldBounds(entity, bounds.LocalBounds, collision.Offset);
                var nearbyEntities = _grid.Query(worldBounds);

                foreach(var other in nearbyEntities)
                {
                    if(entity == other)
                        continue;

                    // prevents duplicate collision events
                    if(entity > other)
                        continue;

                    if(!_ecs.HasComponent<CollisionComponent>(other))
                        continue;

                    ref var otherCollision =
                        ref _ecs.GetComponent<CollisionComponent>(other);

                    if(!canCollide(collision, otherCollision))
                        continue;
                    
                    ref var otherBounds =
                        ref _ecs.GetComponent<Bounds2DComponent>(other);

                    var otherWorldBounds = getWorldBounds(other, otherBounds.LocalBounds, otherCollision.Offset);

                    if(worldBounds.Intersects(otherWorldBounds))
                    {
                        var overlap = calculateOverlap(worldBounds, otherWorldBounds);

                        createCollisionEvent(
                            entity,
                            collision,
                            other,
                            otherCollision,
                            overlap
                        );
                    }
                }
            }
        }

        private Rectangle<float> getWorldBounds(int entity, Rectangle<float> localBounds, Vector2<float> offset)
        {
            ref var transform = ref _ecs.GetComponent<TransformComponent>(entity);

            return new Rectangle<float>
            {
                X = transform.Position.x + localBounds.X + offset.x,
                Y = transform.Position.y + localBounds.Y + offset.y,
                Width = localBounds.Width,
                Height = localBounds.Height
            };
        }

        private bool canCollide(CollisionComponent a, CollisionComponent b)
        {
            return (a.Mask & b.Layer) != CollisionLayer.None &&
                (b.Mask & a.Layer) != CollisionLayer.None;
        }

        private Vector2<float> calculateOverlap(
            Rectangle<float> a,
            Rectangle<float> b
        )
        {
            float aLeft = a.X;
            float aRight = a.X + a.Width;
            float aTop = a.Y;
            float aBottom = a.Y + a.Height;

            float bLeft = b.X;
            float bRight = b.X + b.Width;
            float bTop = b.Y;
            float bBottom = b.Y + b.Height;

            float left = bRight - aLeft;
            float right = aRight - bLeft;
            float top = bBottom - aTop;
            float bottom = aBottom - bTop;

            float overlapX = left < right ? left : -right;
            float overlapY = top < bottom ? top : -bottom;

            if (MathF.Abs(overlapX) < MathF.Abs(overlapY))
                return new Vector2<float>(overlapX, 0);

            return new Vector2<float>(0, overlapY);
        }

        private void createCollisionEvent(
            int entityA,
            CollisionComponent entityACollision,
            int entityB,
            CollisionComponent entityBCollision,
            Vector2<float> overlap
        )
        {
            _ecs.AddComponent(entityA, new CollisionEventComponent
            {
                Target = entityB,
                TargetLayer = entityBCollision.Layer,
                Overlap = overlap
            });

            _ecs.AddComponent(entityB, new CollisionEventComponent
            {
                Target = entityA,
                TargetLayer = entityACollision.Layer,
                Overlap = new(-overlap.x, -overlap.y)
            });
        }
    }
}