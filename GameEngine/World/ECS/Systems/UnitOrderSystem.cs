using GameEngine.SharedInterface;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Gameplay;

namespace GameEngine.World.ECS.Systems
{
    public class UnitOrderSystem : IUpdatable
    {
        private ECSService _ecs;

        public UnitOrderSystem(ECSService ecs)
        {
            _ecs = ecs;
        }

        public void Update(float? delta)
        {
            var entities = _ecs.GetEntitiesWith<UnitOrderComponent>();

            foreach(var entity in entities)
            {
                ref var order = ref _ecs.GetComponent<UnitOrderComponent>(entity);

                switch(order.Type)
                {
                    case UnitOrderType.Move:
                        move(entity, order.Destination);
                    break;
                }
            }
        }

        private void move(int entityId, Vector2<float> direction)
        {
            ref var intent = ref _ecs.GetComponent<MovementIntentComponent>(entityId);
            var transform = _ecs.GetComponent<TransformComponent>(entityId);

            intent.Direction = new Vector2<float>(
                direction.x - transform.Position.x,
                direction.y - transform.Position.y
            );

            if(Vector2<float>.DistanceSquared(
                transform.Position,
                direction
            ) < 4)
            {
                intent.Direction = Vector2<float>.Zero;
                _ecs.RemoveComponent<UnitOrderComponent>(entityId);
            }
        }
    }
}