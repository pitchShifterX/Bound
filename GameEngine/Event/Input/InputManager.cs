using GameEngine.SharedInterface;

namespace GameEngine.Event.Input
{
    public class InputManager : IInputController
    {
        private HashSet<KeyCode> _currentKeys = new();
        private HashSet<KeyCode> _previousKeys = new();
        private Dictionary<int, GamepadState> _gamepads = new();

        public void BeginFrame()
        {
            _previousKeys.Clear();

            foreach(var key in _currentKeys)
                _previousKeys.Add(key);

            foreach(var gamepad in _gamepads.Values)
                gamepad.BeginFrame();
        }

        public void EndFrame(){}

        public void HandleEvent(EngineEvent e)
        {
            if (e is KeyboardEvent keyEvent)
            {
                if (keyEvent.EventType == KeyEventType.Pressed)
                    _currentKeys.Add(keyEvent.Key);
                else if (keyEvent.EventType == KeyEventType.Released)
                    _currentKeys.Remove(keyEvent.Key);
            }
            else if (e is GamepadButtonEvent buttonEvent)
            {
                if (!_gamepads.ContainsKey(buttonEvent.GamepadId))
                    _gamepads[buttonEvent.GamepadId] = new GamepadState();

                if (buttonEvent.IsPressed)
                    _gamepads[buttonEvent.GamepadId].CurrentButtons.Add(buttonEvent.Button);
                else
                    _gamepads[buttonEvent.GamepadId].CurrentButtons.Remove(buttonEvent.Button);
            }
            else if (e is GamepadAxisEvent axisEvent)
            {
                if (!_gamepads.ContainsKey(axisEvent.GamepadId))
                    _gamepads[axisEvent.GamepadId] = new GamepadState();

                _gamepads[axisEvent.GamepadId].AxisValues[axisEvent.Axis] = axisEvent.Value;
            }
        }

        public bool IsKeyPressed(KeyCode key) => _currentKeys.Contains(key);

        public bool WasKeyPressed(KeyCode key) 
            => _currentKeys.Contains(key)
            && !_previousKeys.Contains(key);

        public bool WasKeyReleased(KeyCode key)
            => !_currentKeys.Contains(key)
            && _previousKeys.Contains(key);

        public bool IsGamepadButtonPressed(int id, GamepadButton button)
            => _gamepads.TryGetValue(id, out var pad)
            && pad.CurrentButtons.Contains(button);

        public bool WasGamepadButtonPressed(int id, GamepadButton button)
            => _gamepads.TryGetValue(id, out var pad)
            && pad.CurrentButtons.Contains(button)
            && !pad.PreviousButtons.Contains(button);

        public bool WasGamepadButtonReleased(int id, GamepadButton button)
            => _gamepads.TryGetValue(id, out var pad)
            && !pad.CurrentButtons.Contains(button)
            && pad.PreviousButtons.Contains(button);
        
        public float GetGamepadAxis(int gamepadId, GamepadAxis axis)
            => _gamepads.TryGetValue(gamepadId, out var pad)
                && pad.AxisValues.TryGetValue(axis, out var value)
                    ? value
                    : 0f;
    }

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