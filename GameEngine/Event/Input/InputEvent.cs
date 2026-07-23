namespace GameEngine.Event.Input
{
    /// <summary>
    /// The inherited input events are raw events 
    /// provided by SDL2. In a mod, these will be 
    /// further mapped into Input Actions like unit 
    /// movement, pressed/select for buttons, etc.
    /// </summary>
    public abstract class InputEvent : EngineEvent {}

    public enum KeyCode
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, 
        N, O, P, Q, R, S, T, U, V, W, X, Y, Z,

        Space, Enter, Escape,
        Up, Down, Left, Right
    }

    public enum KeyEventType { Pressed, Released, Repeated }

    public class KeyboardEvent : InputEvent
    {
        public KeyCode Key { get; set; }
        public KeyEventType EventType { get; set; }
    }

    public enum MouseButton
    {
        Left,
        Middle,
        Right
    }

    public enum MouseEventType
    {
        Moved,
        ButtonPressed,
        ButtonReleased,
        Wheel
    }

    public class MouseEvent : InputEvent
    {
        public MouseEventType Type { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }

    public class MouseButtonEvent : MouseEvent
    {
        public MouseButton Button { get; set; }
        public bool IsPressed { get; set; }
    }

    public class MouseMoveEvent : MouseEvent
    {
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
    }

    public class MouseWheelEvent : MouseEvent
    {
        public int ScrollX { get; set; }
        public int ScrollY { get; set; }
    }

    public enum GamepadButton
    {
        A, B, X, Y,
        LB, RB,
        Back, Start,
        LeftStick, RightStick
    }

    public enum GamepadAxis
    {
        LeftX, LeftY,
        RightX, RightY,
        TriggerLeft, TriggerRight
    }

    public enum GamepadEventType
    {
        Connected,
        Disconnected,
        ButtonPressed,
        ButtonReleased,
        AxisMotion
    }

    public class GamepadEvent : InputEvent
    {
        public int GamepadId { get; set; }
        public GamepadEventType EventType { get; set; }
    }

    public class GamepadButtonEvent : GamepadEvent
    {
        public GamepadButton Button { get; set; }
        public bool IsPressed { get; set; }
    }

    public class GamepadAxisEvent : GamepadEvent
    {
        public GamepadAxis Axis { get; set; }

        /// <summary>
        /// Normalized to -1.0 -> 1.0
        /// </summary>
        public float Value { get; set; }
    }
}