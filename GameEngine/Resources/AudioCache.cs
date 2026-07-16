using GameEngine.Exception;
using SDL2;

namespace GameEngine.Resources
{
    public class AudioCache : ResourceCache<Audio>
    {
        public override void Load(string id, string path)
        {
            IntPtr handle = SDL_mixer.Mix_LoadMUS(path);

            if (handle == IntPtr.Zero)
                throw new ResourceException($"Could not load audio: {SDL.SDL_GetError()}");

            var audio = new Audio(handle, id, path);
            Resources.Add(id, audio);
        }
    }
}