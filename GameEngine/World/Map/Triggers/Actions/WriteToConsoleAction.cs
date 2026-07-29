namespace GameEngine.World.Map.Triggers.Actions
{
    public class WriteToConsoleAction : ITriggerAction
    {
        private readonly string _text;

        public WriteToConsoleAction(string text)
        {
            _text = text;
        }

        public TriggerActionResult Execute(IGameplayContext context, float? delta)
        {
            Console.WriteLine(_text);

            return TriggerActionResult.Completed;
        }
    }
}