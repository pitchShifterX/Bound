using GameEngine.Event.Input;
using GameEngine.Mod;

namespace GameEngine.Scene
{
    public abstract class Scene : IScene
    {
        public ISceneContext Context { get; init; }

        public Scene(IModContext modContext)
        {
            Context = new SceneContext(modContext);
        }

        public virtual void Initialize() => Load();
        public abstract void Load();
        public virtual void Unload() => Context.UnloadAll();
        public abstract void ProcessInput(IRecordInput input);
        public abstract void Update(float? delta);
        public abstract void Render();
    }
}