using GameEngine.Event.Input;
using GameEngine.Utilities;

namespace GameEngine.World.Input
{
    public class MouseInputController : IProcessInput
    {
        private IGameplayContext _context;

        public MouseInputController(IGameplayContext context)
        {
            _context = context;
        }

        public void Process(IRecordInput input)
        {
            if(input.WasMouseButtonPressed(MouseButton.Left))
            {
                var start = input.GetMouseDragStart(MouseButton.Left);

                if (start.HasValue)
                    _context?.Selection?.Start(start.Value);
            }

            if(input.IsMouseDragging(MouseButton.Left))
            {
                _context?.Selection?.Update(
                    new Vector2<int>(
                        input.MousePositionX,
                        input.MousePositionY
                    )
                );
            }

            if(input.WasMouseButtonReleased(MouseButton.Left))
            {
                _context?.Selection?.SelectUnits();
                _context?.Selection?.End();
            }

            if(input.WasMouseButtonPressed(MouseButton.Right))
            {
                var worldPosition = _context?.Camera?.View.ScreenPositionToWorldPosition(
                    input.MousePositionX,
                    input.MousePositionY
                );

                if(worldPosition == null) return;
                if(_context?.Selection?.SelectedEntities == null) return;

                _context?.Commands.MoveUnits(
                    _context.Selection.GetControllableEntities(),
                    worldPosition.Value
                );
            }
        }
    }
}