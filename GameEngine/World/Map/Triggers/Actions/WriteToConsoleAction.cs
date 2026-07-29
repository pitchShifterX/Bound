namespace GameEngine.World.Map.Triggers.Actions
{
    public class WriteToConsoleAction : ITriggerAction
    {
        private readonly string _text;

        public WriteToConsoleAction(string text)
        {
            _text = text;
        }

        public void Execute(IGameplayContext context)
        {
            Console.WriteLine(_text);
        }
    }
}