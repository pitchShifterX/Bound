using GameEngine.Exception;
using SDL2;

namespace GameEngine.Resources
{
    public class FontCache : ResourceCache<Font>
    {
        public override void Load(string id, string path)
        {
            IntPtr handle = SDL_ttf.TTF_OpenFont(path, 24);

            if(handle == IntPtr.Zero)
                throw new ResourceException($"Could not load font: {SDL.SDL_GetError()}");

            var font = new Font(handle, id, path);
            
            Resources.Add(id, font);
        }   
    }
}