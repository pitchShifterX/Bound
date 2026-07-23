using GameEngine.SharedInterface;

namespace GameEngine.Event.Input
{
    public class GamepadState : IFrameLifecycle
    {
        public HashSet<GamepadButton> CurrentButtons { get; } = new();
        public HashSet<GamepadButton> PreviousButtons { get; } = new();
        public Dictionary<GamepadAxis, float> AxisValues { get; } = new();

        public void BeginFrame()
        {
            PreviousButtons.Clear();

            foreach(var button in CurrentButtons)
                PreviousButtons.Add(button);
        }

        public void EndFrame(){}
    }
}