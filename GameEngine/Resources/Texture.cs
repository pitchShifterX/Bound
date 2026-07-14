using SDL2;

namespace GameEngine.Resources
{
    public class Texture : Resource
    {
        public IntPtr Handle { get; private set; }

        public Texture(IntPtr handle, string id, string path)
            : base(id, path)
        {
            Handle = handle;
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