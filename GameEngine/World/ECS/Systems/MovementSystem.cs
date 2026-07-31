using GameEngine.SharedInterface;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components;
using GameEngine.World.ECS.Components.Core;

namespace GameEngine.World.ECS.Systems
{
    public class MovementSystem : IUpdatable
    {
        private ECSService _ecs;

        public MovementSystem(ECSService ecs)
        {
            _ecs = ecs;
        }

        public void Update(float? delta)
        {
            if(delta == null) return;

            var entities = _ecs.GetEntitiesWith<MovementIntentComponent, TransformComponent, MovementSpeedComponent>();

            foreach(var entity in entities)
            {
                ref var intent = ref _ecs.GetComponent<MovementIntentComponent>(entity);
                ref var transform = ref _ecs.GetComponent<TransformComponent>(entity);
                ref var speed = ref _ecs.GetComponent<MovementSpeedComponent>(entity);

                var direction = Vector2<float>.Normalize(intent.Direction);
                var newPosition = direction * (speed.Speed * delta.Value);

                transform.Position += newPosition;

                intent.Direction = Vector2<float>.Zero;
            }
        }
    }
}