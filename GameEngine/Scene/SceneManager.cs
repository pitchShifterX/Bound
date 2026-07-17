using GameEngine.Event.Input;
using GameEngine.SharedInterface;

namespace GameEngine.Scene
{
    public class SceneManager : ISceneController
    {
        private readonly Stack<IScene> _stack = new();
        private Func<IScene>? _queuedPush;
        private bool _queuedPop;
        private Func<IScene>? _queuedReplace;

        public IScene? Current => _stack.Count > 0 ? _stack.Peek() : null;

        public void Push(Func<IScene> scene) => _queuedPush = scene;
        public void Pop() => _queuedPop = true;
        public void Replace(Func<IScene> scene) => _queuedReplace = scene;

        public void BeginFrame(){}

        public void EndFrame()
        {
            replaceRequestedSceneAtEndFrame();
            pushRequestedSceneAtEndFrame();
            popRequestedSceneAtEndFrame();
        }

        public void ProcessInput(IRecordInput input)
        {
            Current?.ProcessInput(input);
        }

        public void Update(float? delta)
        {
            Current?.Update(delta);
        }

        public void Render()
        {
            foreach(var scene in _stack.Reverse())
                scene.Render();
        }

        private void replaceRequestedSceneAtEndFrame()
        {
            if(_queuedReplace != null)
            {
                var newScene = _queuedReplace();
                _queuedReplace = null;

                if(Current != null)
                {
                    Current.Unload();
                    _stack.Pop();
                }

                _stack.Push(newScene);
                newScene.Initialize();
                
                return;
            }
        }

        private void pushRequestedSceneAtEndFrame()
        {
            if(_queuedPush != null)
            {
                var newScene = _queuedPush();
                _queuedPush = null;

                if(Current is IPausable pausable)
                    pausable.Pause();
                
                _stack.Push(newScene);
                newScene.Initialize();

                return;
            }
        }

        private void popRequestedSceneAtEndFrame()
        {
            if(_queuedPop)
            {
                _queuedPop = false;
                
                if(Current != null)
                {
                    Current.Unload();
                    _stack.Pop();

                    if(Current is IPausable pausable)
                        pausable.Resume();
                }
            }
        }
    }
}