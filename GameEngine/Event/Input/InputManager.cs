using GameEngine.Utilities;

namespace GameEngine.Event.Input
{
    public class InputManager : IInputController
    {
        private readonly MouseState _mouse = new();
        private HashSet<KeyCode> _currentKeys = [];
        private HashSet<KeyCode> _previousKeys = [];
        private Dictionary<int, GamepadState> _gamepads = [];

        public void BeginFrame()
        {
            _previousKeys.Clear();

            foreach(var key in _currentKeys)
                _previousKeys.Add(key);

            _mouse.BeginFrame();

            foreach(var gamepad in _gamepads.Values)
                gamepad.BeginFrame();
        }

        public void EndFrame(){}

        public void HandleEvent(EngineEvent e)
        {
            switch(e)
            {
                case MouseMoveEvent mouseMove:
                    _mouse.PositionX = mouseMove.PositionX;
                    _mouse.PositionY = mouseMove.PositionY;

                    _mouse.DeltaX += mouseMove.DeltaX;
                    _mouse.DeltaY += mouseMove.DeltaY;
                break;
                case MouseButtonEvent mouseButton:
                    _mouse.PositionX = mouseButton.PositionX;
                    _mouse.PositionY = mouseButton.PositionY;

                    if(mouseButton.IsPressed)
                    {
                        _mouse.CurrentButtons.Add(mouseButton.Button);

                        _mouse.DragStart[mouseButton.Button] =
                            new Vector2<int>(
                                mouseButton.PositionX, mouseButton.PositionY
                            );
                    }
                    else
                    {
                        _mouse.CurrentButtons.Remove(mouseButton.Button);
                        _mouse.DragStart.Remove(mouseButton.Button);
                    }
                break;
                case MouseWheelEvent mouseWheel:
                    _mouse.ScrollX += mouseWheel.ScrollX;
                    _mouse.ScrollY += mouseWheel.ScrollY;
                break;
                case KeyboardEvent keyEvent:
                    if (keyEvent.EventType == KeyEventType.Pressed)
                        _currentKeys.Add(keyEvent.Key);
                    else if (keyEvent.EventType == KeyEventType.Released)
                        _currentKeys.Remove(keyEvent.Key);
                break;
                case GamepadButtonEvent buttonEvent:
                    if (!_gamepads.ContainsKey(buttonEvent.GamepadId))
                        _gamepads[buttonEvent.GamepadId] = new GamepadState();

                    if (buttonEvent.IsPressed)
                        _gamepads[buttonEvent.GamepadId].CurrentButtons.Add(buttonEvent.Button);
                    else
                        _gamepads[buttonEvent.GamepadId].CurrentButtons.Remove(buttonEvent.Button);
                break;
                case GamepadAxisEvent axisEvent:
                    if (!_gamepads.ContainsKey(axisEvent.GamepadId))
                        _gamepads[axisEvent.GamepadId] = new GamepadState();

                    _gamepads[axisEvent.GamepadId].AxisValues[axisEvent.Axis] = axisEvent.Value;
                break;
            }
        }

        public bool IsMouseButtonPressed(MouseButton button)
            => _mouse.CurrentButtons.Contains(button);

        public bool WasMouseButtonPressed(MouseButton button)
            => _mouse.CurrentButtons.Contains(button)
            && !_mouse.PreviousButtons.Contains(button);

        public bool WasMouseButtonReleased(MouseButton button)
            => !_mouse.CurrentButtons.Contains(button)
            && _mouse.PreviousButtons.Contains(button);

        public int GetMouseDragX(MouseButton button)
        {
            if(!_mouse.DragStart.TryGetValue(button, out var position))
                return 0;
            
            return _mouse.PositionX - position.x;
        }

        public int GetMouseDragY(MouseButton button)
        {
            if(!_mouse.DragStart.TryGetValue(button, out var position))
                return 0;

            return _mouse.PositionY - position.y;
        }

        public Vector2<int>? GetMouseDragStart(MouseButton button)
        {
            if (_mouse.DragStart.TryGetValue(button, out var position))
                return position;

            return null;
        }

        public bool IsMouseDragging(MouseButton button)
        {
            if (!IsMouseButtonPressed(button))
                return false;

            if (!_mouse.DragStart.TryGetValue(button, out var position))
                return false;

            return Math.Abs(_mouse.PositionX - position.x) >= _mouse.DragThreshold
                || Math.Abs(_mouse.PositionY - position.y) >= _mouse.DragThreshold;
        }

        public int MousePositionX => _mouse.PositionX;
        public int MousePositionY => _mouse.PositionY;

        public int MouseDeltaX => _mouse.DeltaX;
        public int MouseDeltaY => _mouse.DeltaY;

        public int MouseScrollX => _mouse.ScrollX;
        public int MouseScrollY => _mouse.ScrollY;

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
}