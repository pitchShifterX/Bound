using SDL2;
using GameEngine.Event.Input;
using GameEngine.Utilities;

namespace GameEngine.Event
{
    public class EventManager(IReceiveEvents receiver) : IEventController
    {
        public bool IsQuitting { get; set; } = false;

        private readonly IReceiveEvents _receiver = receiver;
        private readonly Dictionary<SDL.SDL_Keycode, KeyCode> _keyMap = BuildKeyMap();

        public void PollEvents()
        {
            while(SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
            {
                var translatedEvent = Translate(e);

                if(translatedEvent != null)
                {
                    _receiver.HandleEvent(translatedEvent);

                    if(translatedEvent is QuitEvent)
                    {
                        IsQuitting = true;
                    }
                }
            }
        }

        public EngineEvent? Translate(SDL.SDL_Event e)
        {
            return e.type switch
            {
                SDL.SDL_EventType.SDL_QUIT
                    => new QuitEvent(),

                SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN
                    => TranslateMouseButtonEvent(e.button, isPressed: true),
                
                SDL.SDL_EventType.SDL_MOUSEBUTTONUP
                    => TranslateMouseButtonEvent(e.button, isPressed: false),

                SDL.SDL_EventType.SDL_MOUSEMOTION
                    => TranslateMouseMoveEvent(e.motion),

                SDL.SDL_EventType.SDL_MOUSEWHEEL
                    => TranslateMouseWheelEvent(e.wheel),
                
                SDL.SDL_EventType.SDL_KEYDOWN
                    => TranslateKeyboardEvent(e.key, isPressed: true),

                SDL.SDL_EventType.SDL_KEYUP 
                    => TranslateKeyboardEvent(e.key, isPressed: false),

                SDL.SDL_EventType.SDL_JOYBUTTONDOWN
                    => TranslateGamepadButtonEvent(e.jbutton, isPressed: true),

                SDL.SDL_EventType.SDL_JOYBUTTONUP
                    => TranslateGamepadButtonEvent(e.jbutton, isPressed: false),
                
                SDL.SDL_EventType.SDL_JOYAXISMOTION
                    => TranslateGamepadAxisEvent(e.jaxis),

                SDL.SDL_EventType.SDL_JOYDEVICEADDED
                    => new GamepadEvent 
                    { 
                        GamepadId = e.jdevice.which, 
                        EventType = GamepadEventType.Connected 
                    },

                SDL.SDL_EventType.SDL_JOYDEVICEREMOVED
                    => new GamepadEvent 
                    { 
                        GamepadId = e.jdevice.which, 
                        EventType = GamepadEventType.Disconnected 
                    },

                _ => null
            };
        }

        private InputEvent? TranslateMouseButtonEvent(SDL.SDL_MouseButtonEvent buttonEvent, bool isPressed)
        {
            MouseButton? parsedButton = buttonEvent.button switch
            {
                (byte)SDL.SDL_BUTTON_LEFT => MouseButton.Left,
                (byte)SDL.SDL_BUTTON_MIDDLE => MouseButton.Middle,
                (byte)SDL.SDL_BUTTON_RIGHT => MouseButton.Right,
                _ => null
            };
            
            if(parsedButton == null)
                return null;
            
            return new MouseButtonEvent
            {
                Button = parsedButton.Value,
                IsPressed = isPressed,
                PositionX = buttonEvent.x,
                PositionY = buttonEvent.y
            };
        }

        private InputEvent? TranslateMouseMoveEvent(SDL.SDL_MouseMotionEvent motion)
        {
            return new MouseMoveEvent
            {
                PositionX = motion.x,
                PositionY = motion.y,
                DeltaX = motion.xrel,
                DeltaY = motion.yrel
            };
        }

        private InputEvent? TranslateMouseWheelEvent(SDL.SDL_MouseWheelEvent wheel)
        {
            int scrollX = wheel.x;
            int scrollY = wheel.y;
            
            return new MouseWheelEvent
            {
                ScrollX = scrollX,
                ScrollY = scrollY
            };
        }

        private InputEvent? TranslateKeyboardEvent(SDL.SDL_KeyboardEvent keyEvent, bool isPressed)
        {
            if(_keyMap.TryGetValue(keyEvent.keysym.sym, out var keyCode))
            {
                return new KeyboardEvent
                {
                    Key = keyCode,
                    EventType = isPressed
                        ? (keyEvent.repeat != 0 ? KeyEventType.Repeated : KeyEventType.Pressed)
                        : KeyEventType.Released
                };
            }

            return null;
        }

        private InputEvent? TranslateGamepadButtonEvent(SDL.SDL_JoyButtonEvent buttonEvent, bool isPressed)
        {
            var button = (GamepadButton)buttonEvent.button;

            return new GamepadButtonEvent
            {
                GamepadId = buttonEvent.which,
                EventType = isPressed ? GamepadEventType.ButtonPressed : GamepadEventType.ButtonReleased,
                Button = button,
                IsPressed = isPressed
            };
        }

        private InputEvent? TranslateGamepadAxisEvent(SDL.SDL_JoyAxisEvent axisEvent)
        {
            const float axisValues = 32768f;
            var normalized = axisEvent.axisValue / axisValues;

            const float deadzone = 0.15f;
            if(Math.Abs(normalized) < deadzone)
                normalized = 0;

            var axis = (GamepadAxis)axisEvent.axis;

            return new GamepadAxisEvent
            {
                GamepadId = axisEvent.which,
                EventType = GamepadEventType.AxisMotion,
                Axis = axis,
                Value = normalized
            };
        }

        private static Dictionary<SDL.SDL_Keycode, KeyCode> BuildKeyMap()
        {
            return new Dictionary<SDL.SDL_Keycode, KeyCode>
            {
                { SDL.SDL_Keycode.SDLK_a, KeyCode.A },
                { SDL.SDL_Keycode.SDLK_b, KeyCode.B },
                { SDL.SDL_Keycode.SDLK_c, KeyCode.C },
                { SDL.SDL_Keycode.SDLK_d, KeyCode.D },
                { SDL.SDL_Keycode.SDLK_e, KeyCode.E },
                { SDL.SDL_Keycode.SDLK_f, KeyCode.F },
                { SDL.SDL_Keycode.SDLK_g, KeyCode.G },
                { SDL.SDL_Keycode.SDLK_h, KeyCode.H },
                { SDL.SDL_Keycode.SDLK_i, KeyCode.I },
                { SDL.SDL_Keycode.SDLK_j, KeyCode.J },
                { SDL.SDL_Keycode.SDLK_k, KeyCode.K },
                { SDL.SDL_Keycode.SDLK_l, KeyCode.L },
                { SDL.SDL_Keycode.SDLK_m, KeyCode.M },
                { SDL.SDL_Keycode.SDLK_n, KeyCode.N },
                { SDL.SDL_Keycode.SDLK_o, KeyCode.O },
                { SDL.SDL_Keycode.SDLK_p, KeyCode.P },
                { SDL.SDL_Keycode.SDLK_q, KeyCode.Q },
                { SDL.SDL_Keycode.SDLK_r, KeyCode.R },
                { SDL.SDL_Keycode.SDLK_s, KeyCode.S },
                { SDL.SDL_Keycode.SDLK_t, KeyCode.T },
                { SDL.SDL_Keycode.SDLK_u, KeyCode.U },
                { SDL.SDL_Keycode.SDLK_v, KeyCode.V },
                { SDL.SDL_Keycode.SDLK_w, KeyCode.W },
                { SDL.SDL_Keycode.SDLK_x, KeyCode.X },
                { SDL.SDL_Keycode.SDLK_y, KeyCode.Y },
                { SDL.SDL_Keycode.SDLK_z, KeyCode.Z },

                { SDL.SDL_Keycode.SDLK_SPACE, KeyCode.Space },
                { SDL.SDL_Keycode.SDLK_RETURN, KeyCode.Enter },
                { SDL.SDL_Keycode.SDLK_ESCAPE, KeyCode.Escape },

                { SDL.SDL_Keycode.SDLK_UP, KeyCode.Up },
                { SDL.SDL_Keycode.SDLK_DOWN, KeyCode.Down },
                { SDL.SDL_Keycode.SDLK_LEFT, KeyCode.Left },
                { SDL.SDL_Keycode.SDLK_RIGHT, KeyCode.Right },
            };
        }
    }
}