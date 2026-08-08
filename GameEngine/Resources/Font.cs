using GameEngine.Utilities;
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

        public Vector2<int> CalculateSize(string text)
        {
            if(SDL_ttf.TTF_SizeUTF8(Handle, text, out int width, out int height) == 0)
            {
                return new(width, height);
            }

            return Vector2<int>.Zero;
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