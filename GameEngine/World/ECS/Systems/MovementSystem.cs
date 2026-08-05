using GameEngine.SharedInterface;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Spatial;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Spatial;

namespace GameEngine.World.ECS.Systems
{
    public class MovementSystem : IUpdatable
    {
        private readonly ECSService _ecs;
        private readonly SpatialHashGrid _grid;
        private readonly TerrainService _terrain;

        public MovementSystem(ECSService ecs, SpatialHashGrid grid, TerrainService terrain)
        {
            _ecs = ecs;
            _grid = grid;
            _terrain = terrain;
        }

        public void Update(float? delta)
        {
            if(delta == null) return;

            var entities = _ecs.GetEntitiesWith<MovementIntentComponent, TransformComponent, MovementSpeedComponent>();

            foreach(var entity in entities)
            {
                ref var intent = ref _ecs.GetComponent<MovementIntentComponent>(entity);

                // update Vector2 to support
                if(intent.Direction.x == 0 && intent.Direction.y == 0)
                    continue;

                ref var transform = ref _ecs.GetComponent<TransformComponent>(entity);
                ref var speed = ref _ecs.GetComponent<MovementSpeedComponent>(entity);

                var direction = Vector2<float>.Normalize(intent.Direction);
                var newPosition = direction * (speed.Speed * delta.Value);
                var proposedPosition = transform.Position + newPosition;

                if(canMove(entity, proposedPosition))
                {
                    transform.Position += newPosition;
                }

                intent.Direction = Vector2<float>.Zero;
            }
        }

        private bool canMove(int entity, Vector2<float> position)
        {
            ref var collision = ref _ecs.GetComponent<CollisionComponent>(entity);
            ref var bounds = ref _ecs.GetComponent<Bounds2DComponent>(entity);

            var proposedBounds = getWorldBounds(
                position,
                bounds.LocalBounds,
                collision.Offset
            );

            if(!_terrain.IsWalkable(proposedBounds))
                return false;

            foreach (var other in _grid.Query(proposedBounds))
            {
                if (other == entity)
                    continue;

                if (!_ecs.HasComponent<CollisionComponent>(other))
                    continue;

                ref var otherCollision = ref _ecs.GetComponent<CollisionComponent>(other);

                if (!canCollide(collision, otherCollision))
                    continue;

                ref var otherTransform = ref _ecs.GetComponent<TransformComponent>(other);
                ref var otherBounds = ref _ecs.GetComponent<Bounds2DComponent>(other);

                var otherWorldBounds = getWorldBounds(
                    otherTransform.Position,
                    otherBounds.LocalBounds,
                    otherCollision.Offset
                );

                if (proposedBounds.Intersects(otherWorldBounds))
                    return false;
            }

            return true;
        }

        private bool canCollide(CollisionComponent a, CollisionComponent b)
        {
            return (a.Mask & b.Layer) != CollisionLayer.None &&
                (b.Mask & a.Layer) != CollisionLayer.None;
        }

        private Rectangle<float> getWorldBounds(
            Vector2<float> position,
            Rectangle<float> localBounds,
            Vector2<float> offset
        )
        {
            return new Rectangle<float>
            {
                X = position.x + localBounds.X + offset.x,
                Y = position.y + localBounds.Y + offset.y,
                Width = localBounds.Width,
                Height = localBounds.Height
            };
        }
    }
}