using GameEngine.Resources;

namespace GameEngine.Scene
{
    public class SceneManager : ISceneController
    {
        private Func<IScene>? _queuedScene;
        public IScene? CurrentScene { get; private set; }

        public void SetInitial(IScene scene)
        {
            CurrentScene = scene;
        }

        public void RequestSceneChange(Func<IScene> scene)
        {
            _queuedScene = scene;
        }

        public void BeginFrame()
        {
            
        }

        public void EndFrame()
        {
            if(_queuedScene != null && CurrentScene != null)
            {
                CurrentScene.Unload();
                
                CurrentScene = _queuedScene();
                _queuedScene = null;

                CurrentScene.Initialize();
            }
        }
    }
}