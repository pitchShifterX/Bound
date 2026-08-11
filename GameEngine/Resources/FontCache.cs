using GameEngine.Exception;
using SDL2;

namespace GameEngine.Resources
{
    public class FontCache : ResourceCache<Font>
    {
        public override void Load(string id, string path)
        {
            LoadWithSize(id, path, 24);
        }
        
        public void LoadWithSize(string id, string path, int size)
        {
            IntPtr handle = SDL_ttf.TTF_OpenFont(path, size);

            if(handle == IntPtr.Zero)
                throw new ResourceException($"Could not load font: {SDL.SDL_GetError()}");

            var font = new Font(handle, id, path, size);
            
            Resources.Add(id, font);
        }
    }
}