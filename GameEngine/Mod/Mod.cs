using GameEngine.Event;
using GameEngine.Event.Input;
using GameEngine.Render;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.Settings;
using GameEngine.Window;
using SDL2;

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
            Context.ResourceManager = new ResourceManager(Context.RendererManager.Renderer);
            Context.SceneManager = new SceneManager();

            IsRunning = true;
        }

        public virtual void Start()
        {
            try
            {
                Run();
            }
            catch(System.Exception e)
            {
                Console.WriteLine($"Fatal error: {e}");
            }
            finally
            {
                Close();
            }
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

        public virtual void Close()
        {
            Context?.ResourceManager?.UnloadAllResourceCaches();
            Context?.RendererManager?.Destroy();
            Context?.WindowManager?.Destroy();

            SDL.SDL_Quit();
        }
    }
}