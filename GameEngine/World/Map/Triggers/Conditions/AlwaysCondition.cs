namespace GameEngine.World.Map.Triggers.Conditions
{
    public class AlwaysCondition : ITriggerCondition
    {
        public bool Evaluate(IGameplayContext context) => true;
    }
}