
namespace GameEngine.Utilities
{
    public struct Vector2<T>(T xValue, T yValue)
        where T : System.Numerics.INumber<T>
    {
        public T x = xValue;
        public T y = yValue;

        public static Vector2<T> Zero => new(T.Zero, T.Zero);

        public static Vector2<T> operator +(Vector2<T> a, Vector2<T> b)
        {
            return new Vector2<T>(a.x + b.x, a.y + b.y);
        }

        public static Vector2<T> operator -(Vector2<T> a, Vector2<T> b)
        {
            return new Vector2<T>(a.x - b.x, a.y - b.y);
        }

        public static Vector2<T> operator *(Vector2<T> vector, T scalar)
        {
            return new Vector2<T>(
                vector.x * scalar,
                vector.y * scalar
            );
        }

        public static T LengthSquared(Vector2<T> vector)
        {
            return (vector.x * vector.x) + (vector.y * vector.y);
        }

        public static double Length(Vector2<T> vector)
        {
            return Math.Sqrt(
                double.CreateChecked(LengthSquared(vector))
            );
        }

        public static Vector2<T> Normalize(Vector2<T> vector)
        {
            var lengthSquared = LengthSquared(vector);

            if (lengthSquared == T.Zero)
                return Zero;

            var length = Math.Sqrt(double.CreateChecked(lengthSquared));

            return new Vector2<T>(
                T.CreateChecked(double.CreateChecked(vector.x) / length),
                T.CreateChecked(double.CreateChecked(vector.y) / length)
            );
        }

        public static T DistanceSquared(Vector2<T> a, Vector2<T> b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;

            return (dx * dx) + (dy * dy);
        }
    }
}