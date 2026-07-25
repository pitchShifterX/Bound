namespace GameEngine.World.Map.Triggers
{
    public class Trigger
    {
        public required string Name { get; set; }
        public bool IsPreserved { get; set; }
        public bool HasExecuted { get; set; }

        public List<Func<bool>> Conditions { get; set; } = [];
        public List<Action> Actions { get; set; } = [];

        public bool Evaluate()
        {
            if(!IsPreserved && HasExecuted) return false;

            foreach(var condition in Conditions)
            {
                if(!condition()) return false;
            }

            return true;
        }

        public void Execute()
        {
            foreach(var action in Actions)
            {
                action();
            }

            HasExecuted = true;
        }
    }
}