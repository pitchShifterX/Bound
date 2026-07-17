using GameEngine.Event.Input;
using GameEngine.SharedInterface;

namespace GameEngine.Scene
{
    public interface ISceneController : 
        IStackController<IScene>, 
        IUpdatable, 
        IRenderable, 
        IFrameLifecycle
    {
        public void ProcessInput(IRecordInput inputManager);
    }
}