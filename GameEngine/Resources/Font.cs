using SDL2;

namespace GameEngine.Resources
{
    public class Font : Resource
    {
        public Font(IntPtr handle, string id, string path)
            : base(id, path)
        {
            Handle = handle;
        }

        protected override void Destroy()
        {
            if(Handle != IntPtr.Zero)
            {
                SDL_ttf.TTF_CloseFont(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}