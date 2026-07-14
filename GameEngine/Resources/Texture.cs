using GameEngine.Utilities;
using SDL2;

namespace GameEngine.Resources
{
    public class Texture : Resource
    {
        public Vector2<int> Size { get; }

        public Texture(IntPtr handle, string id, string path)
            : base(id, path)
        {
            Handle = handle;

            SDL.SDL_QueryTexture(
                handle,
                out _,
                out _,
                out var width,
                out var height
            );

            Size = new(width, height);
        }

        protected override void Destroy()
        {
            if(Handle != IntPtr.Zero)
            {
                SDL.SDL_DestroyTexture(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}