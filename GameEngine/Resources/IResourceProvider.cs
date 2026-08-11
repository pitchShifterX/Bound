namespace GameEngine.Resources
{
    public interface IResourceProvider
    {
        public T? GetById<T>(string id) where T : Resource;
        public T? TryGetById<T>(string id) where T : Resource;
    }
}