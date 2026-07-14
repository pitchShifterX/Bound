using System.Diagnostics;
using GameEngine.Event;
using GameEngine.Event.Input;
using GameEngine.Exception;
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

            Initialize();
        }

        public virtual void Initialize()
        {
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                throw new SDLInitException($"Could not initialize SDL: {SDL.SDL_GetError()}");
            }

            var imgFlags = 
                SDL_image.IMG_InitFlags.IMG_INIT_JPG |
                SDL_image.IMG_InitFlags.IMG_INIT_PNG;

            var imgInit = SDL_image.IMG_Init(imgFlags);

            if((imgInit & (int)imgFlags) != (int)imgFlags)
            {
                throw new SDLInitException($"Could not initialize SDL_image: {SDL.SDL_GetError()}");
            }

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

        public virtual void Run()
        {
            var stopwatch = Stopwatch.StartNew();
            double previous = stopwatch.Elapsed.TotalSeconds;
            const double fixedDelta = 1.0 / 60.0;
            double timeAccumulator = 0;

            while(IsRunning)
            {
                double now = stopwatch.Elapsed.TotalSeconds;
                double frameDelta = now - previous;
                previous = now;
                timeAccumulator += frameDelta;

                var maxFramerate = Context.SettingsManager?.Settings.MaxFramerate ?? 60;
                double minFrameMs = (maxFramerate > 0) ? 1000.0 / maxFramerate : 0;

                Context.InputManager?.BeginFrame();
                Context.EventManager?.PollEvents();

                if(Context.EventManager == null || Context.EventManager.IsQuitting)
                {
                    IsRunning = false;
                    break;
                }

                Context.SceneManager?.BeginFrame();
                Context.SceneManager?.ProcessInput(Context.InputManager!);

                while (timeAccumulator >= fixedDelta)
                {
                    Context.SceneManager?.Update((float)fixedDelta);
                    timeAccumulator -= fixedDelta;
                }

                Context.RendererManager?.Clear();
                Context.SceneManager?.Render();
                Context.RendererManager?.Present();
                
                Context.SceneManager?.EndFrame();
                Context.InputManager?.EndFrame();

                if(minFrameMs > 0)
                {
                    var elapsedMs = (stopwatch.Elapsed.TotalSeconds - now) * 1000.0;
                    var sleepMs = minFrameMs - elapsedMs;

                    if(sleepMs > 1)
                        SDL.SDL_Delay((uint)sleepMs);
                }
            }
        }

        public virtual void Close()
        {
            Context?.ResourceManager?.UnloadAllResourceCaches();
            Context?.RendererManager?.Destroy();
            Context?.WindowManager?.Destroy();

            SDL_image.IMG_Quit();
            SDL.SDL_Quit();
        }
    }
}