using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components.Gameplay;

namespace GameEngine.World.Input.Commands
{
    public class CommandService
    {
        private ECSService _ecs;

        public CommandService(ECSService ecs)
        {
            _ecs = ecs;
        }

        public void MoveUnits(IEnumerable<int> units, Vector2<float> destination)
        {
            foreach(var unit in units)
            {
                if(_ecs.HasComponent<UnitOrderComponent>(unit))
                {
                    ref var order = ref _ecs.GetComponent<UnitOrderComponent>(unit);
                    
                    order = new UnitOrderComponent
                    {
                        Type = UnitOrderType.Move,
                        Destination = destination
                    };
                }
                else
                {
                    _ecs.AddComponent(unit, new UnitOrderComponent
                    {
                        Type = UnitOrderType.Move,
                        Destination = destination
                    });
                }
            }
        }
    }
}