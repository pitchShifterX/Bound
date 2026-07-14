using GameEngine.Exception;

namespace GameEngine.Resources
{
    public class ResourceManager : IResourceController
    {
        private readonly Dictionary<Type, object> _caches = [];

        public ResourceManager(IntPtr renderer)
        {
            _caches[typeof(Texture)] = new TextureCache(renderer);
        }

        public void Load<T>(string id, string path) where T : Resource
        {
            if(!_caches.TryGetValue(typeof(T), out var cache))
                throw new ResourceException($"Could not load {typeof(T).Name} resource with id: {id}");
            
            ((ResourceCache<T>)cache).Load(id, path);
        }

        public void Load<T>(T resource) where T : Resource
        {
            if(!_caches.TryGetValue(typeof(T), out var cache))
                throw new ResourceException($"Could not load {typeof(T).Name} resource with id: {resource.Id}");
            
            ((ResourceCache<T>)cache).Load(resource);
        }

        public T? GetById<T>(string id) where T : Resource
        {
            if(!_caches.TryGetValue(typeof(T), out var cache))
                throw new ResourceException($"Could not find {typeof(T).Name} resource with id: {id}");

            return ((ResourceCache<T>)cache).GetById(id);
        }

        public void Unload<T>(Type type, string id) where T : Resource
        {
            if (!_caches.TryGetValue(type, out var cache))
                return;

            ((ResourceCache<T>)cache).UnloadById(id);
        }

        public void UnloadById<T>(string id) where T : Resource
        {
            if(!_caches.TryGetValue(typeof(T), out var cache))
                throw new ResourceException($"{typeof(T).Name} resource not found with id and could not be unloaded: {id}");
            
            ((ResourceCache<T>)cache).UnloadById(id);
        }

        public void UnloadAllFromCache<T>() where T : Resource
        {
            if(!_caches.TryGetValue(typeof(T), out var cache))
                throw new ResourceException($"Could not find cache of specified type to unload all resources: {typeof(T).Name}");
            
            ((ResourceCache<T>)cache).UnloadAll();
        }

        public void UnloadAllResourceCaches()
        {
            foreach(var cache in _caches.Values)
            {
                ((IResourceCache)cache).UnloadAll();
            }
        }
    }
}