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
            if(Resources.ContainsKey(id))
                throw new ResourceException($"Texture resource already exists: {id}");

            IntPtr handle = SDL_image.IMG_LoadTexture(_renderer, path);

            if(handle == IntPtr.Zero)
                throw new ResourceException($"Could not load texture: {SDL.SDL_GetError()}");
            
            var texture = new Texture(handle, id, path);

            Resources.Add(id, texture);
        }

        public Texture LoadFromSurface(string id, IntPtr surface, string? generatedPath = null)
        {
            if(Resources.ContainsKey(id))
                throw new ResourceException($"Texture resource already exists: {id}");

            if(surface == IntPtr.Zero)
                throw new ResourceException("Surface pointer is zero");

            var texHandle = SDL.SDL_CreateTextureFromSurface(_renderer, surface);
            if(texHandle == IntPtr.Zero)
                throw new ResourceException($"Could not create texture from surface: {SDL.SDL_GetError()}");

            var path = generatedPath ?? $"_generated://{id}";
            var texture = new Texture(texHandle, id, path);

            Resources.Add(id, texture);

            return texture;
        }

        public Texture LoadFromHandle(string id, IntPtr textureHandle, string? generatedPath = null)
        {
            if (Resources.ContainsKey(id))
                throw new ResourceException($"Texture resource already exists: {id}");

            if (textureHandle == IntPtr.Zero)
                throw new ResourceException("Texture handle is zero");

            var path = generatedPath ?? $"_generated://{id}";

            var texture = new Texture(textureHandle, id, path);

            Resources.Add(id, texture);

            return texture;
        }
    }
}