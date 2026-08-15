namespace GameEngine.World.Map.Triggers.Conditions
{
    public class AlwaysCondition : ITriggerCondition
    {
        public bool Evaluate(IWorldContext context) => true;
    }
}