using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Triggers
{
    public class TriggerLuaBinder
    {
        private TriggerRegistry _registry;

        public TriggerLuaBinder(TriggerRegistry registry)
        {
            _registry = registry;
        }

        public void Bind(Script script)
        {
            foreach(var condition in _registry.Conditions)
            {
                var factory = condition.Value;

                script.Globals[condition.Key] = DynValue.NewCallback(
                    (ctx, args) => DynValue.FromObject(
                        script,
                        factory(new TriggerArguments(args))
                    ),
                    condition.Key
                );

            }

            foreach(var action in _registry.Actions)
            {
                var factory = action.Value;

                script.Globals[action.Key] = DynValue.NewCallback(
                    (ctx, args) => DynValue.FromObject(
                        script,
                        factory(new TriggerArguments(args))
                    ),
                    action.Key
                );
            }
        }
    }
}