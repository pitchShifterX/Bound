using GameEngine.Utilities;

namespace GameEngine.World.Map.Triggers.Actions
{
    public class WriteToConsoleAction : ITriggerAction
    {
        private readonly string _text;

        public WriteToConsoleAction(string text)
        {
            _text = text;
        }

        public TriggerActionResult Execute(IWorldContext context, float? delta)
        {
            Log.Info(_text);

            return TriggerActionResult.Completed;
        }
    }
}