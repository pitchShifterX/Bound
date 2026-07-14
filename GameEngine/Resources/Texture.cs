using SDL2;

namespace GameEngine.Resources
{
    public class Texture : Resource
    {
        public Texture(IntPtr handle, string id, string path)
            : base(id, path)
        {
            Handle = handle;
        }

        protected override void Destroy()
        {
            if(Handle != IntPtr.Zero)
            {
                Console.WriteLine("Texture destroyed");

                SDL.SDL_DestroyTexture(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}