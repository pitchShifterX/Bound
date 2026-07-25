using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.Unit;

namespace GameEngine.World.Map.Triggers
{
    public class TriggerEngine : IUpdatable
    {
        private Dictionary<string, TriggerGroup> _triggerGroups = [];
        private ECSService _ecs;
        private UnitPrefabRegistry _unitRegistry;

        public TriggerEngine(ECSService service, UnitPrefabRegistry unitRegistry)
        {
            _ecs = service;
            _unitRegistry = unitRegistry;
        }

        public void Update(float? delta)
        {
            foreach(var triggerGroup in _triggerGroups.Values)
            {
                triggerGroup.Execute();
            }
        }
    }
}