using GameEngine.Resources;

namespace GameEngine.Scene
{
    public interface ISceneContext
    {
        public void Load<T>(T resource) where T : Resource;
        public T? GetById<T>(string id) where T : Resource;
        public void UnloadById<T>(string id) where T : Resource;
        public void UnloadAll();
    }
}