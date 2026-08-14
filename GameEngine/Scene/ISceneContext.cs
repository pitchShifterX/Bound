using GameEngine.Audio;
using GameEngine.Event;
using GameEngine.Graphics.Rendering;
using GameEngine.Resources;
using GameEngine.Settings;
using GameEngine.Utilities;
using SDL2;

namespace GameEngine.Scene
{
    public interface ISceneContext : IControlMusic, IRenderContext
    {
        public IModPath Paths { get; }
        public ISettingsController SettingsManager { get; }
        public Settings.Settings Settings { get; }

        public UIEventBus UIEvents { get; }

        public void PushScene(Func<IScene> factory);
        public void PopScene();
        public void ReplaceScene(Func<IScene> factory);
        
        public void Load<T>(string id, string path) where T : Resource;
        public void LoadFont(string id, string path, int size);
        public void UnloadById<T>(string id) where T : Resource;
        public void UnloadAll();

        public void GetTextTexture(Font font, string text, SDL.SDL_Color color);
        public void ReleaseTextTexture(Font font, string text, SDL.SDL_Color color);

        public void QuitMod();
    }
}