using GameEngine.Utilities;

namespace GameEngine.Resources
{
    public abstract class ResourceCache<T> : IResourceCache<T> 
        where T : Resource
    {
        protected Dictionary<string, T> Resources { get; init; } = [];

        public abstract void Load(string id, string path);

        public virtual void Load(T resource)
        {
            if(Resources.ContainsKey(resource.Id))
            {
                Log.Warn($"There was an attempt to load an existing {typeof(T).Name} resource: {resource.Id}");

                return;
            }

            resource.Load();
            Resources.Add(resource.Id, resource);
        }

        public T? GetById(string id)
        {
            if (!Resources.TryGetValue(id, out var resource))
            {
                Log.Warn($"{typeof(T).Name} resource does not exist in resource cache: {id}");

                return null;
            }

            return resource;
        }

        public void UnloadById(string id)
        {
            if (!Resources.TryGetValue(id, out var resource))
            {
                Log.Warn($"{typeof(T).Name} resource does not exist in resource cache and could not be unloaded: {id}");

                return;
            }

            Console.WriteLine($"{id} was unloaded.");

            resource.Dispose();
            Resources.Remove(id);
        }

        public void UnloadAll()
        {
            foreach(var resource in Resources.Values)
            {
                Console.WriteLine($"{resource.Id} was unloaded.");
                resource.Dispose();
            }

            Resources.Clear();
        }
    }
}