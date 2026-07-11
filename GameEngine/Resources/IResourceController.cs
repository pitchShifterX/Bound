namespace GameEngine.Resources
{
    public interface IResourceController
    {
        public void Load<T>(T resource) where T : Resource;
        public T? GetById<T>(string id) where T : Resource;
        public void UnloadById<T>(string id) where T : Resource;
        public void UnloadAllFromCache<T>() where T : Resource;
        public void UnloadAllResourceCaches();
    }
}