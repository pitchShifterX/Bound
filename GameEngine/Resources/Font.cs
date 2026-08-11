using GameEngine.Utilities;
using SDL2;

namespace GameEngine.Resources
{
    public class Font : Resource
    {
        public int Size { get; init; }

        public Font(IntPtr handle, string id, string path, int size)
            : base(id, path)
        {
            Handle = handle;
            Size = size;
        }

        public Vector2<int> CalculateSize(string text)
        {
            if(Handle == IntPtr.Zero) return Vector2<int>.Zero;
            
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