using GameEngine.Exception;
using SDL2;

namespace GameEngine.Resources
{
    public class TextureCache : ResourceCache<Texture>
    {
        private IntPtr _renderer;

        public TextureCache(IntPtr renderer)
        {
            _renderer = renderer;
        }

        public override void Load(string id, string path)
        {
            var texturePath = Path.Combine(AppContext.BaseDirectory, path);
            IntPtr handle = SDL_image.IMG_LoadTexture(_renderer, texturePath);

            if(handle == IntPtr.Zero)
                throw new ResourceException($"Could not load texture: {SDL.SDL_GetError()}");
            
            var texture = new Texture(handle, id, path);

            Resources.Add(id, texture);
        }
    }
}