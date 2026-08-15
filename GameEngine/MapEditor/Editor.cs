using GameEngine.Event.Input;
using GameEngine.Scene;
using GameEngine.SharedInterface;
using GameEngine.World.Input;

namespace GameEngine.MapEditor
{
    public abstract class Editor : IUpdatable, IRenderable, IProcessInput
    {
        protected ISceneContext SceneContext;
        protected EditorContext Context;

        protected GameRegistries Registries => SceneContext.Registries;

        public Editor(ISceneContext sceneContext)
        {
            SceneContext = sceneContext;
            
            Context = new EditorContext(SceneContext, new Random());
        }

        public abstract void Start();

        public virtual void Process(IRecordInput input)
        {
            Context.Process(input);
        }

        public virtual void Update(float? delta)
        {
            Context.Update(delta);
        }

        public virtual void Render()
        {
            Context.Render();
        }

        public virtual void OnOpenFile(string filter)
        {
            var path = Context.File!.OpenDialog(filter);

            if(path != null)
                Context.LoadMap(path);
        }
    }
}