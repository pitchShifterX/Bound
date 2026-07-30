using System.Numerics;

namespace GameEngine.Utilities
{
    public struct Rectangle<T> where T : INumber<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Width { get; set; }
        public T Height { get; set; }

        public Vector2<T> Center => new()
        {
            x = X + Width / T.CreateChecked(2),
            y = Y + Height / T.CreateChecked(2)
        };

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

        public readonly bool Contains(Vector2<T> position)
        {
            return position.x >= X &&
                position.x < X + Width &&
                position.y >= Y &&
                position.y < Y + Height;
        }

        public Rectangle<TTo> To<TTo>()
            where TTo : INumber<TTo>
        {
            return new Rectangle<TTo>(
                TTo.CreateChecked(X),
                TTo.CreateChecked(Y),
                TTo.CreateChecked(Width),
                TTo.CreateChecked(Height)
            );
        }
    }
}