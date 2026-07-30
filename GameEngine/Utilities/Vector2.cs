
namespace GameEngine.Utilities
{
    public struct Vector2<T>(T xValue, T yValue)
        where T : System.Numerics.INumber<T>
    {
        public T x = xValue;
        public T y = yValue;

        public static Vector2<T> Zero => new(T.Zero, T.Zero);

        public static T DistanceSquared(Vector2<T> a, Vector2<T> b)
        {
            var dx = a.x - b.x;
            var dy = a.y - b.y;

            return (dx * dx) + (dy * dy);
        }
    }
}