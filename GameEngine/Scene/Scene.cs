using GameEngine.Event;
using GameEngine.Event.Input;
using GameEngine.Resources;

namespace GameEngine.Scene
{
    public abstract class Scene : IScene
    {
        public ISceneContext Context { get; init; }

        public Scene(IResourceController resourceController)
        {
            Context = new SceneContext(resourceController);
        }

        public virtual void Initialize()
        {
            Load();
        }

        public abstract void Load();

        public virtual void Unload()
        {
            Context.UnloadAll();
        }

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