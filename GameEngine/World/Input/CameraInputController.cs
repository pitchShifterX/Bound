using GameEngine.Event.Input;
using GameEngine.World.Rendering.Cameras;

namespace GameEngine.World.Input
{
    public class CameraInputController : IProcessInput
    {
        private ICameraController _camera;

        public CameraInputController(ICameraController camera)
        {
            _camera = camera;
        }

        public void Process(IRecordInput input)
        {
            if(_camera == null) return;
            
            if(input.IsKeyPressed(KeyCode.Up))
                _camera.MoveDirection(Direction.Up);
            
            if(input.IsKeyPressed(KeyCode.Down))
                _camera.MoveDirection(Direction.Down);
            
            if(input.IsKeyPressed(KeyCode.Left))
                _camera.MoveDirection(Direction.Left);
            
            if(input.IsKeyPressed(KeyCode.Right))
                _camera.MoveDirection(Direction.Right);

            if(input.MouseScrollY > 0)
                _camera.SetZoom(2.5f);
            
            if(input.MouseScrollY < 0)
                _camera.SetZoom(2);
        }
    }
}