using GameEngine.Event.Input;
using GameEngine.World.Input.Commands;
using GameEngine.World.Rendering.Cameras;

namespace GameEngine.World.Input
{
    public class InputService
    {
        private readonly SelectionService _selection;
        private readonly CommandService _commands;
        private readonly CameraContext _camera;

        private CameraInputController _cameraInputController;
        private MouseInputController _mouse;
        private GamepadInputController _gamepad;

        public InputService(
            SelectionService selection, 
            CameraContext camera,
            CommandService commands
        )
        {
            _selection = selection;
            _commands = commands;
            _camera = camera;

            _cameraInputController = new CameraInputController(_camera.Controller);

            _mouse = new MouseInputController(
                _selection,
                _camera,
                _commands
            );

            _gamepad = new GamepadInputController();
        }

        public void Process(IRecordInput input)
        {
            _cameraInputController.Process(input);
            _mouse.Process(input);
            _gamepad.Process(input);
        }
    }
}