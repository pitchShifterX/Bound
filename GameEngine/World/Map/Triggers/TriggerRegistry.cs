namespace GameEngine.World.Map.Triggers
{
    public class TriggerRegistry
    {
        private Dictionary<string, Func<bool>> _conditions = [];
        private Dictionary<string, Action> _actions = [];

        public Dictionary<string, Func<bool>> Conditions => _conditions;
        public Dictionary<string, Action> Actions => _actions;

        public void RegisterCondition(string name, Func<bool> condition)
        {
            _conditions.Add(name, condition);
        }

        public void RegisterAction(string name, Action action)
        {
            _actions.Add(name, action);
        }
    }
}