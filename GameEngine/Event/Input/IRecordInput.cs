namespace GameEngine.Event.Input
{
    public interface IRecordInput
    {
        public bool IsKeyPressed(KeyCode key);
        public bool WasKeyPressed(KeyCode key);
        public bool WasKeyReleased(KeyCode key);

        public bool IsGamepadButtonPressed(int id, GamepadButton button);
        public bool WasGamepadButtonPressed(int id, GamepadButton button);
        public bool WasGamepadButtonReleased(int id, GamepadButton button);
    }
}