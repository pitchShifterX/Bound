using GameEngine.Event.Input;
using GameEngine.Utilities;

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

            if (input.WasMouseButtonPressed(MouseButton.Left))
            {
                var start = input.GetMouseDragStart(MouseButton.Left);

                if (start.HasValue)
                    _context.Selection.Start(start.Value);
            }

            if (input.IsMouseDragging(MouseButton.Left))
            {
                _context.Selection.Update(
                    new Vector2<int>(
                        input.MousePositionX,
                        input.MousePositionY
                    )
                );
            }

            if (input.WasMouseButtonReleased(MouseButton.Left))
            {
                _context.Selection.SelectUnits();
                _context.Selection.End();
            }
        }
    }
}