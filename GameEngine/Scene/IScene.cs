using GameEngine.Event.Input;
using GameEngine.SharedInterface;

namespace GameEngine.Scene
{
    public interface IScene : IInitializable, IUpdatable, IRenderable, ILoadable
    {
        public void ProcessInput(InputManager inputHandler);
    }
}