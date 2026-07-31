using GameEngine.Event.Input;

namespace GameEngine.World.Input
{
    public class InputService
    {
        private IGameplayContext _context;

        private CameraInputController _camera;
        private MouseInputController _mouse;
        private GamepadInputController _gamepad;

        public InputService(IGameplayContext context)
        {
            _context = context;

            _camera = new CameraInputController(_context.Camera!.Controller);
            _mouse = new MouseInputController(_context);
            _gamepad = new GamepadInputController(_context);
        }

        public void Process(IRecordInput input)
        {
            _camera.Process(input);
            _mouse.Process(input);
            _gamepad.Process(input);
        }
    }
}