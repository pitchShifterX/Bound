using GameEngine.SharedInterface;

namespace GameEngine.World.Map.Triggers
{
    public class TriggerEngine : IUpdatable
    {
        private Dictionary<string, TriggerGroup> _triggerGroups = [];
        private IGameplayContext _context;

        public TriggerEngine(IGameplayContext context)
        {
            _context = context;
        }

        public void AddTriggerGroup(TriggerGroup group)
        {
            _triggerGroups[group.Name] = group;
        }

        public TriggerGroup? GetTriggerGroupByName(string name)
        {
            if(!_triggerGroups.TryGetValue(name, out var triggerGroup))
                return null;
            
            return triggerGroup;
        }

        public void Update(float? delta)
        {
            foreach(var triggerGroup in _triggerGroups.Values)
            {
                triggerGroup.Update(delta, _context);
            }
        }
    }
}