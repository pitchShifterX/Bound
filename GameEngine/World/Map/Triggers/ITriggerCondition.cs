namespace GameEngine.World.Map.Triggers
{
    public interface ITriggerCondition
    {
        public bool Evaluate(IGameplayContext context);
    }
}