using GameEngine.Resources;

namespace GameEngine.Scene
{
    public class SceneContext : ISceneContext
    {
        private IResourceController _resourceManager { get; }

        private readonly HashSet<(Type Type, string Id)> _loadedResources = [];

        public SceneContext(IResourceController resourceController)
        {
            _resourceManager = resourceController;
        }

        public void Load<T>(T resource) where T : Resource
        {
            _resourceManager.Load(resource);

            _loadedResources.Add((typeof(T), resource.Id));
        }

        public T? GetById<T>(string id) where T : Resource
            => _resourceManager.GetById<T>(id);

        public void UnloadById<T>(string id) where T : Resource
        {
            if (!_loadedResources.Remove((typeof(T), id)))
                return;

            _resourceManager.UnloadById<T>(id);
        }

        public void UnloadAll()
        {
            foreach (var (type, id) in _loadedResources)
            {
                switch(type.Name)
                {
                    case nameof(Texture):
                        _resourceManager.UnloadAll<Texture>();
                    break;
                }
            }

            _loadedResources.Clear();
        }
    }
}