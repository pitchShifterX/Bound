using GameEngine.Event.Input;
using GameEngine.Utilities;
using GameEngine.World.Input.Commands;
using GameEngine.World.Rendering.Cameras;

namespace GameEngine.World.Input
{
    public class MouseInputController : IProcessInput
    {
        private readonly SelectionService _selection;
        private readonly CameraContext _camera;
        private readonly CommandService? _commands;

        public MouseInputController(
            SelectionService selection,
            CameraContext camera
        )
        {
            _selection = selection;
            _camera = camera;
        }

        public MouseInputController(
            SelectionService selection, 
            CameraContext camera,
            CommandService commands
        )
        {
            _selection = selection;
            _camera = camera;
            _commands = commands;
        }

        public void Process(IRecordInput input)
        {
            if(input.WasMouseButtonPressed(MouseButton.Left))
            {
                var start = input.GetMouseDragStart(MouseButton.Left);

                if (start.HasValue)
                    _selection.Start(start.Value);
            }

            if(input.IsMouseDragging(MouseButton.Left))
            {
                _selection.Update(
                    new Vector2<int>(
                        input.MousePositionX,
                        input.MousePositionY
                    )
                );
            }

            if(input.WasMouseButtonReleased(MouseButton.Left))
            {
                _selection.SelectUnits();
                _selection.End();
            }

            if(input.WasMouseButtonPressed(MouseButton.Right))
            {
                var worldPosition = _camera.View.ScreenPositionToWorldPosition(
                    input.MousePositionX,
                    input.MousePositionY
                );

                if(_selection.SelectedEntities == null) return;

                _commands?.MoveUnits(
                    _selection.GetControllableEntities(),
                    worldPosition
                );
            }
        }
    }
}