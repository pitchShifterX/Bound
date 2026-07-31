using GameEngine.Event.Input;

namespace GameEngine.World.Input
{
    public class GamepadInputController : IProcessInput
    {
        private IGameplayContext _context;

        public GamepadInputController(IGameplayContext context)
        {
            _context = context;
        }

        public void Process(IRecordInput input)
        {
            
        }
    }
}