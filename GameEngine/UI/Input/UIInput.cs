using GameEngine.Utilities;

namespace GameEngine.UI.Input
{
    // most likely will refactor to merge gamepad functionality
    public readonly struct UIInput
    {
        public Vector2<float> MousePosition { get; }

        public bool MouseLeftPressed { get; }
        public bool MouseLeftReleased { get; }
        public bool MouseLeftDown { get; }

        public UIInput(
            Vector2<float> mousePosition,
            bool mouseLeftPressed,
            bool mouseLeftReleased,
            bool mouseLeftDown
        )
        {
            MousePosition = mousePosition;

            MouseLeftPressed = mouseLeftPressed;
            MouseLeftReleased = mouseLeftReleased;
            MouseLeftDown = mouseLeftDown;
        }
    }
}