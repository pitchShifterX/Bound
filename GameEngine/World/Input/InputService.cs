using GameEngine.Event.Input;

namespace GameEngine.World.Input
{
    public class InputService
    {
        private IGameplayContext _context;

        private CameraInputController _camera;

        public InputService(IGameplayContext context)
        {
            _context = context;
            _camera = new CameraInputController(_context.Camera!.Controller);
        }

        public void Process(IRecordInput input)
        {
            _camera.Process(input);
        }
    }
}