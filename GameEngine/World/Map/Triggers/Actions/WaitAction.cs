namespace GameEngine.World.Map.Triggers.Actions
{
    public class WaitAction : ITriggerAction
    {
        private float _seconds;
        private float _elapsed;

        public WaitAction(float seconds)
        {
            _seconds = seconds;
        }

        public TriggerActionResult Execute(IWorldContext context, float? delta)
        {
            if(delta == null) return TriggerActionResult.Completed;

            _elapsed += delta.Value;

            if (_elapsed >= _seconds)
                return TriggerActionResult.Completed;

            return TriggerActionResult.Running;
        }

        public void Reset()
        {
            _elapsed = 0;
        }
    }
}