using GameEngine.Event;
using GameEngine.Event.Input;

namespace GameEngine.Scene
{
    public abstract class Scene : IScene
    {
        public Scene()
        {
        }

        /// <summary>
        /// Begin loading resources
        /// </summary>
        public virtual void Initialize()
        {
            OnLoad();
        }

        public abstract void OnLoad();
        public abstract void OnUnload();

        public void ProcessInput(InputManager inputHandler)
        {
            
        }

        public void Update()
        {
            
        }

        public void Render()
        {
            
        }
    }
}