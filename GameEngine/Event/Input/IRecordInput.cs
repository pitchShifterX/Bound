namespace GameEngine.Event.Input
{
    public interface IRecordInput
    {
        public bool IsMouseButtonPressed(MouseButton button);
        public bool WasMouseButtonPressed(MouseButton button);
        public bool WasMouseButtonReleased(MouseButton button);
        public int MousePositionX { get; }
        public int MousePositionY { get; }
        public int MouseDeltaX { get; }
        public int MouseDeltaY { get; }
        public int MouseScrollX { get; }
        public int MouseScrollY { get; }
        
        public bool IsKeyPressed(KeyCode key);
        public bool WasKeyPressed(KeyCode key);
        public bool WasKeyReleased(KeyCode key);

        public bool IsGamepadButtonPressed(int id, GamepadButton button);
        public bool WasGamepadButtonPressed(int id, GamepadButton button);
        public bool WasGamepadButtonReleased(int id, GamepadButton button);
    }
}