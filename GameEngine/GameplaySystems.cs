using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Systems;

namespace GameEngine
{
    public class GameplaySystems : IUpdatable
    {
        private ECSService _ecs;

        private MovementSystem _movementSystem;
        private UnitOrderSystem _unitOrderSystem;

        public GameplaySystems(ECSService ecs)
        {
            _ecs = ecs;

            _movementSystem = new MovementSystem(_ecs);
            _unitOrderSystem = new UnitOrderSystem(_ecs);
        }

        public void Update(float? delta)
        {
            _unitOrderSystem.Update(delta);
            _movementSystem.Update(delta);
        }
    }
}