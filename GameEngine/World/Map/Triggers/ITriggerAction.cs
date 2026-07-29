namespace GameEngine.World.Map.Triggers
{
    public interface ITriggerAction
    {
        public TriggerActionResult Execute(IGameplayContext context, float? delta);
    }
}