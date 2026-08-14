using GameEngine.Event.Input;
using GameEngine.Mod;
using GameEngine.UI;
using GameEngine.UI.Themes;

namespace GameEngine.Scene
{
    public abstract class Scene : IScene
    {
        public ISceneContext Context { get; init; }

        public virtual IUITheme UITheme { get; init; } = new DefaultUITheme();

        public UIManager UI { get; init; }
        
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
            UI = new UIManager(Context, UITheme);
        }

        public virtual void Initialize() => Load();
        public abstract void Load();
        public abstract void BuildUI();
        public abstract void ProcessInput(IRecordInput input);
        public abstract void Update(float? delta);
        public abstract void Render();

        public virtual void Unload()
        {
            Context.UnloadAll();

            UI.Unsubscribe();
        }
    }
}