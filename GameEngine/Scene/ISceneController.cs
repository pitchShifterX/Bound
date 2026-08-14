using GameEngine.Event.Input;
using GameEngine.SharedInterface;

namespace GameEngine.Scene
{
    public interface ISceneController : 
        IStackController<IScene>, 
        IUpdatable, 
        IRenderable, 
        IFrameLifecycle,
        ILoadable
    {
        public void ProcessInput(IRecordInput inputManager);
    }
}