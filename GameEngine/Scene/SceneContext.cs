using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Settings;
using GameEngine.Utilities;
using SDL2;

namespace GameEngine.Scene
{
    public class SceneContext : ISceneContext
    {
        private IModContext _modContext { get; }
        private IResourceController _resourceManager => _modContext.ResourceManager!;

        public IntPtr Renderer => _modContext.RendererManager!.Renderer;
        public IModPath Paths => _modContext.Paths!;
        public ISettingsController SettingsManager => _modContext.SettingsManager!;
        public Settings.Settings Settings => _modContext.SettingsManager!.Settings;

        /// <summary>
        /// Resources loaded by this scene. These 
        /// resources will be automatically unloaded.
        /// </summary>
        private readonly HashSet<(Type Type, string Id)> _loadedResources = [];

        /// <summary>
        /// Context for safely accessing and using 
        /// the game's managers.
        /// </summary>
        /// <param name="modContext"></param>
        public SceneContext(IModContext modContext)
        {
            _modContext = modContext;
        }

        /// <summary>
        /// <para>Push a scene to overlay the current scene.</para>
        /// <para>Use this if you want to maintain existing 
        /// resources.</para>
        /// </summary>
        /// <param name="item"></param>
        public void PushScene(Func<IScene> item)
            => _modContext.SceneManager?.Push(item);

        /// <summary>
        /// Remove the top scene from the stack.
        /// </summary>
        public void PopScene()
            => _modContext.SceneManager?.Pop();
        
        /// <summary>
        /// Replace the current scene with a new scene.
        /// </summary>
        /// <param name="item"></param>
        public void ReplaceScene(Func<IScene> item)
            => _modContext.SceneManager?.Replace(item);

        /// <summary>
        /// Load a resource (image, sound, font, etc). 
        /// 
        /// The resource is added to a local hashset 
        /// to remember what was added by this scene.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="path"></param>
        public void Load<T>(string id, string path) where T : Resource
        {
            _resourceManager.Load<T>(id, path);

            _loadedResources.Add((typeof(T), id));
        }

        /// <summary>
        /// Get a resource.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T? GetById<T>(string id) where T : Resource
            => _resourceManager.GetById<T>(id);

        /// <summary>
        /// Unload a resource by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public void UnloadById<T>(string id) where T : Resource
        {
            if (!_loadedResources.Remove((typeof(T), id)))
                return;

            _resourceManager.UnloadById<T>(id);
        }

        /// <summary>
        /// Unloads all resources that were 
        /// added by this scene.
        /// </summary>
        public void UnloadAll()
        {
            foreach (var (type, id) in _loadedResources)
            {
                switch(type.Name)
                {
                    case nameof(Audio):
                        _resourceManager.UnloadById<Resources.Audio>(id);
                    break;
                    case nameof(Font):
                        _resourceManager.UnloadById<Font>(id);
                    break;
                    case nameof(Texture):
                        _resourceManager.UnloadById<Texture>(id);
                    break;
                }
            }

            _loadedResources.Clear();
        }

        public void DrawText(Font font, string text, SDL.SDL_Color color, SDL.SDL_Rect destination)
        {
            _modContext.RendererManager?.DrawDynamicText(font, text, color, destination);
        }

        public void DrawTexture(IntPtr texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination)
        {
            _modContext.RendererManager?.Draw(texture, source, destination);
        }

        public void DrawTexture(Texture texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination)
        {
            DrawTexture(texture.Handle, source, destination);
        }

        public void PlayMusic(string id, int loop = -1)
        {
            _modContext.AudioManager?.PlayMusic(id, loop);
        }

        public void ResumeMusic() => _modContext.AudioManager?.ResumeMusic();
        public void PauseMusic() => _modContext.AudioManager?.PauseMusic();
        public void StopMusic() => _modContext.AudioManager?.StopMusic();

        public void QuitMod()
        {
            if(_modContext.EventManager != null)
            {
                _modContext.EventManager.IsQuitting = true;
            }
        }
    }
}