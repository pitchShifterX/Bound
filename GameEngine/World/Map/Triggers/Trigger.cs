using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Triggers
{
    public class Trigger
    {
        private int _currentAction;

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

            if (_currentAction >= Actions.Count)
            {
                if(IsPreserved)
                {
                    _currentAction = 0;

                    ResetActions();
                }
                else
                {
                    HasExecuted = true;
                }

                return;
            }

            var result = Actions[_currentAction].Execute(context, delta);

            if (result == TriggerActionResult.Completed)
            {
                _currentAction++;
            }
        }

        private void ResetActions()
        {
            foreach(var action in Actions)
            {
                action.Reset();
            }
        }
    }
}