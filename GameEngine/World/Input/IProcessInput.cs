using GameEngine.Event.Input;

namespace GameEngine.World.Input
{
    public interface IProcessInput
    {
        public void Process(IRecordInput input);
    }
}