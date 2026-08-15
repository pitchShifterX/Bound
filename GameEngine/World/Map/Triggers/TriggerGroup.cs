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

        public void Update(float? delta, IWorldContext context)
        {
            if(!IsEnabled) return;

            foreach(var trigger in Triggers)
            {
                trigger.Update(delta, context);
            }
        }
    }
}