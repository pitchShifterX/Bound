using GameEngine.Mod;
using GameEngine.Resources;

namespace GameEngine.Scene
{
    public class SceneContext : ISceneContext
    {
        private IModContext _modContext { get; }
        private IResourceController _resourceManager => _modContext.ResourceManager!;

        public IntPtr Renderer => _modContext.RendererManager!.Renderer;
        public ISceneController SceneManager => _modContext.SceneManager!;

        /// <summary>
        /// Resources loaded by this scene. These 
        /// resources will be automatically unloaded.
        /// </summary>
        private readonly HashSet<(Type Type, string Id)> _loadedResources = [];

        public SceneContext(IModContext modContext)
        {
            _modContext = modContext;
        }

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
            Console.WriteLine("loading resource in scene context");

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
                    case nameof(Texture):
                        Console.WriteLine($"unloading {id}");
                        
                        _resourceManager.UnloadById<Texture>(id);
                    break;
                }
            }

            _loadedResources.Clear();
        }
    }
}