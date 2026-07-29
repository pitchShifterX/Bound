using System.Numerics;

namespace GameEngine.Utilities
{
    public struct Rectangle<T> where T : INumber<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Width { get; set; }
        public T Height { get; set; }

        public Rectangle(
            T x,
            T y,
            T width,
            T height
        )
        {
            X = x;
            Y = y;
            Width = width;
            Height= height;
        }

        public readonly bool Intersects(Rectangle<T> other)
        {
            return X < other.X + other.Width &&
               X + Width > other.X &&
               Y < other.Y + other.Height &&
               Y + Height > other.Y;
        }
    }
}