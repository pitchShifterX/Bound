using GameEngine.Audio;
using GameEngine.Resources;
using GameEngine.Utilities;
using SDL2;

namespace GameEngine.Scene
{
    public interface ISceneContext : IControlMusic
    {
        public IntPtr Renderer { get; }
        public IModPath Paths { get; }

        public void PushScene(Func<IScene> factory);
        public void PopScene();
        public void ReplaceScene(Func<IScene> factory);
        
        public void Load<T>(string id, string path) where T : Resource;
        public T? GetById<T>(string id) where T : Resource;
        public void UnloadById<T>(string id) where T : Resource;
        public void UnloadAll();

        public void DrawText(Font font, string text, SDL.SDL_Color color, SDL.SDL_Rect destination);
        public void DrawTexture(Texture texture, SDL.SDL_Rect? source, SDL.SDL_Rect destination);

        public void QuitMod();
    }
}