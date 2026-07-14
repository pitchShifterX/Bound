using GameEngine.Resources;

namespace GameEngine.Scene
{
    public interface ISceneContext
    {
        public IntPtr Renderer { get; }
        public ISceneController SceneManager { get; }
        
        public void Load<T>(string id, string path) where T : Resource;
        public T? GetById<T>(string id) where T : Resource;
        public void UnloadById<T>(string id) where T : Resource;
        public void UnloadAll();
    }
}