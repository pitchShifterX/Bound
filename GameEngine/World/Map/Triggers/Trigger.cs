using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Triggers
{
    public class Trigger
    {
        public required string Name { get; set; }
        public bool IsPreserved { get; set; }

        [MoonSharpHidden]
        public bool HasExecuted { get; set; }

        public List<ITriggerCondition> Conditions { get; } = [];
        public List<ITriggerAction> Actions { get; } = [];

        public void Update(float? delta, IGameplayContext context)
        {
            if (!IsPreserved && HasExecuted)
            return;

            foreach (var condition in Conditions)
            {
                if (!condition.Evaluate(context))
                    return;
            }

            foreach (var action in Actions)
            {
                action.Execute(context);
            }

            HasExecuted = true;
        }
    }
}