using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Triggers
{
    public class TriggerRegistry
    {
        private readonly Dictionary<string, Func<TriggerArguments, ITriggerCondition>> _conditions = [];
        private readonly Dictionary<string, Func<TriggerArguments, ITriggerAction>> _actions = [];

        public IReadOnlyDictionary<string, Func<TriggerArguments, ITriggerCondition>>
            Conditions => _conditions;
        
        public IReadOnlyDictionary<string, Func<TriggerArguments, ITriggerAction>>
            Actions => _actions;

        public void RegisterCondition<T>(
            string name,
            Func<TriggerArguments, T> factory)
            where T : class, ITriggerCondition
        {
            UserData.RegisterType<T>();

            _conditions[name] = args => factory(args);
        }

        public void RegisterAction<T>(
            string name,
            Func<TriggerArguments, T> factory)
            where T : class, ITriggerAction
        {
            UserData.RegisterType<T>();

            _actions[name] = args => factory(args);
        }

        public ITriggerCondition CreateCondition(
            string name,
            TriggerArguments args)
        {
            if (!_conditions.TryGetValue(name, out var factory))
                throw new InvalidOperationException(
                    $"Unknown trigger condition {name}."
                );

            return factory(args);
        }

        public ITriggerAction CreateAction(
            string name,
            TriggerArguments args)
        {
            if (!_actions.TryGetValue(name, out var factory))
                throw new InvalidOperationException(
                    $"Unknown trigger action {name}."
                );

            return factory(args);
        }
    }
}