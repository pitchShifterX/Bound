namespace GameEngine.World.Map.Triggers
{
    public class TriggerGroup
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsEnabled { get; set; }
        public List<Trigger> Triggers = [];

        public TriggerGroup(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public void Execute()
        {
            if(!IsEnabled) return;

            foreach(var trigger in Triggers)
            {
                if(trigger.Evaluate())
                {
                    trigger.Execute();
                }
            }
        }
    }
}