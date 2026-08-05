using GameEngine.SharedInterface;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Spatial;
using GameEngine.World.Spatial;

namespace GameEngine.World.ECS.Systems
{
    public class SpatialSystem : IUpdatable
    {
        private readonly ECSService _ecs;
        private readonly SpatialHashGrid _grid;

        private Dictionary<int, Rectangle<float>> _previousBounds = [];

        public SpatialSystem(
            ECSService ecs,
            SpatialHashGrid grid
        )
        {
            _ecs = ecs;
            _grid = grid;
        }

        public void Update(float? delta)
        {
            var entities = _ecs.GetEntitiesWith<TransformComponent, Bounds2DComponent>();

            foreach(var entity in entities)
            {
                ref var transform =
                    ref _ecs.GetComponent<TransformComponent>(entity);

                ref var bounds =
                    ref _ecs.GetComponent<Bounds2DComponent>(entity);

                var worldBounds = getWorldBounds(
                    transform,
                    bounds.LocalBounds
                );

                if(_previousBounds.TryGetValue(entity, out var oldBounds))
                {
                    if(oldBounds != worldBounds)
                    {
                        _grid.Update(
                            entity,
                            oldBounds,
                            worldBounds
                        );
                    }
                }
                else
                {
                    _grid.Insert(
                        entity,
                        worldBounds
                    );
                }

                _previousBounds[entity] = worldBounds;
            }
        }

        private Rectangle<float> getWorldBounds(
            TransformComponent transform,
            Rectangle<float> localBounds
        )
        {
            return new Rectangle<float>
            {
                X = transform.Position.x + localBounds.X,
                Y = transform.Position.y + localBounds.Y,
                Width = localBounds.Width,
                Height = localBounds.Height
            };
        }
    }
}