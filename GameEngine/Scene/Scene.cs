using GameEngine.Event.Input;
using GameEngine.Mod;

namespace GameEngine.Scene
{
    public abstract class Scene : IScene
    {
        public ISceneContext Context { get; init; }
        
        /// <summary>
        /// <para>
        /// The entire context of the mod. While you can access the
        /// managers of the mod, it's recommended to use SceneContext 
        /// instead, as it's a filter for scene needs.
        /// </para>
        /// 
        /// <para>
        /// This is used for passing around the mod context when 
        /// attempting to push or replace a scene.
        /// </para>
        /// </summary>
        protected IModContext ModContext { get; }

        public Scene(IModContext modContext)
        {
            ModContext = modContext;
            Context = new SceneContext(ModContext);
        }

        public virtual void Initialize() => Load();
        public abstract void Load();
        public virtual void Unload() => Context.UnloadAll();
        public abstract void ProcessInput(IRecordInput input);
        public abstract void Update(float? delta);
        public abstract void Render();
    }
}