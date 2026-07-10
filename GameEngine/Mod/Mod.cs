using GameEngine.Event;
using GameEngine.Event.Input;
using GameEngine.Render;
using GameEngine.Settings;
using GameEngine.Window;

namespace GameEngine.Mod
{
    public abstract class Mod<TConfig> : IMod
        where TConfig : ModConfiguration
    {
        protected bool IsRunning { get; set; }
        public TConfig Config { get; }
        public IModContext Context { get; init; }

        public Mod(TConfig config)
        {
            Config = config;

            Context = new ModContext();
        }

        public virtual void Initialize()
        {
            Context.SettingsManager = new SettingsManager(Config.SettingPath);
            
            Context.WindowManager = new WindowManager();
            Context.WindowManager.Create(
                Config.ModName,
                Config.WindowResolution
            );

            Context.RendererManager = new RendererManager(
                Context.SettingsManager.Settings,
                Context.WindowManager.Window
            );

            Context.InputManager = new InputManager();
            Context.EventManager = new EventManager(Context.InputManager);

            IsRunning = true;
        }

        public abstract void Update();
        public abstract void Render();

        public virtual void Run()
        {
            if(Context.InputManager == null)
                throw new System.Exception("Input manager not initialized");

            if(Context.EventManager == null)
                throw new System.Exception("Event manager not initialized");
            
            while(IsRunning)
            {
                Context.InputManager.BeginFrame();
                Context.EventManager.PollEvents();

                if(Context.EventManager.IsQuitting)
                {
                    IsRunning = false;
                    break;
                }

                Update();
                Render();
            }
        }
    }
}