namespace GameEngine.Resources
{
    public class ResourceManager : IResourceController
    {
        private readonly Dictionary<string, Resource> _textures = new();
    }
}