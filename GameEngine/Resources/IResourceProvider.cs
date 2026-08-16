namespace GameEngine.Resources
{
    public interface IResourceProvider
    {
        public T? GetById<T>(string id) where T : Resource;
        public bool TryGetById<T>(string id, out T? resource) where T : Resource;
    }
}