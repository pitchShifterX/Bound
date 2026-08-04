namespace GameEngine.World.Map.Triggers.Conditions
{
    public class ElapsedTimeCondition : ITriggerCondition
    {
        private float _seconds;

        public ElapsedTimeCondition(float seconds)
        {
            _seconds = seconds;
        }

        public bool Evaluate(IGameplayContext context)
        {
            return context.Time.World.ElapsedSeconds >= _seconds;
        }
    }
}