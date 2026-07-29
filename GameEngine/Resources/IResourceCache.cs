namespace GameEngine.Resources
{
    public interface IResourceCache
    {
        public void UnloadAll();
    }

    public interface IResourceCache<T> : IResourceCache
        where T : Resource
    {
        public void Load(T resource);
        public void UnloadById(string id);
    }
}