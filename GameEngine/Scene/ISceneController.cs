using GameEngine.Event.Input;
using GameEngine.SharedInterface;

namespace GameEngine.Scene
{
    public interface ISceneController : IUpdatable, IRenderable, IFrameLifecycle
    {
        public IScene? Current { get; }
        public void PushScene(Func<IScene> scene);
        public void PopScene();
        public void ReplaceScene(Func<IScene> scene);
        public void ProcessInput(IRecordInput inputManager);
    }
}