using GameEngine.Event.Input;
using GameEngine.SharedInterface;

namespace GameEngine.Scene
{
    public interface IScene : IInitializable, IUpdatable, IRenderable, ILoadable
    {
        public ISceneContext Context { get; init; }
        public void ProcessInput(IRecordInput input);
    }
}