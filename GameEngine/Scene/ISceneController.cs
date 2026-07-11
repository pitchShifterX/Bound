using GameEngine.SharedInterface;

namespace GameEngine.Scene
{
    public interface ISceneController : IUpdatable, IRenderable
    {
        public IScene? CurrentScene { get; }
        public void SetInitial(IScene scene);
        public void RequestSceneChange(Func<IScene> scene);
    }
}