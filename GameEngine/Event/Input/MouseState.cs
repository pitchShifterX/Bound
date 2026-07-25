using GameEngine.SharedInterface;

namespace GameEngine.Event.Input
{
    public record struct MousePoint(int X, int Y);

    public class MouseState : IFrameLifecycle
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public int DeltaX { get; set; }
        public int DeltaY { get; set; }

        public int ScrollX { get; set; }
        public int ScrollY { get; set; }

        public HashSet<MouseButton> CurrentButtons { get; } = [];
        public HashSet<MouseButton> PreviousButtons { get; } = [];

        public Dictionary<MouseButton, MousePoint> DragStart = [];
        public readonly int DragThreshold = 5;

        public void BeginFrame()
        {
            PreviousButtons.Clear();

            foreach (var button in CurrentButtons)
                PreviousButtons.Add(button);

            DeltaX = 0;
            DeltaY = 0;
            ScrollX = 0;
            ScrollY = 0;
        }

        public void EndFrame(){}
    }
}