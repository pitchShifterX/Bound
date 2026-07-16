using SDL2;

namespace GameEngine.Resources
{
    public class Audio : Resource
    {
        public Audio(IntPtr handle, string id, string path)
            : base(id, path)
        {
            Handle = handle;
        }

        protected override void Destroy()
        {
            if (Handle != IntPtr.Zero)
            {
                SDL_mixer.Mix_FreeMusic(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}