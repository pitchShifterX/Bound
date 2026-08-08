using GameEngine.Event.Input;
using GameEngine.SharedInterface;
using GameEngine.UI;

namespace GameEngine.Scene
{
    public interface IScene : IInitializable, IUpdatable, IRenderable, ILoadable
    {
        public ISceneContext Context { get; init; }
        public UIManager UI { get; init; }

        public void ProcessInput(IRecordInput input);
        public void BuildUI();
    }
}