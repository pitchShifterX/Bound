using GameEngine.Audio;
using GameEngine.Resources;
using SDL2;

namespace GameEngine.Scene
{
    public interface ISceneContext : IControlMusic
    {
        public IntPtr Renderer { get; }
        public ISceneController SceneManager { get; }
        
        public void Load<T>(string id, string path) where T : Resource;
        public T? GetById<T>(string id) where T : Resource;
        public void UnloadById<T>(string id) where T : Resource;
        public void UnloadAll();

        public void DrawTexture(Texture texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination);
    }
}