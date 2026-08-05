namespace GameEngine.World.Spatial
{
    public readonly struct GridCell : IEquatable<GridCell>
    {
        public readonly int X;
        public readonly int Y;

        public GridCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(GridCell other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is GridCell other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}