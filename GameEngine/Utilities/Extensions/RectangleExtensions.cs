using SDL2;

namespace GameEngine.Utilities.Extensions
{
    public static class RectangleExtensions
    {
        public static SDL.SDL_Rect ToSDLRect<T>(
            this Rectangle<T> rectangle
        ) where T : System.Numerics.INumber<T>
        {
            return new SDL.SDL_Rect
            {
                x = int.CreateChecked(rectangle.X),
                y = int.CreateChecked(rectangle.Y),
                w = int.CreateChecked(rectangle.Width),
                h = int.CreateChecked(rectangle.Height)
            };
        }
    }
}