namespace GameEngine.Utilities
{
    public readonly record struct Rectangle<T>(
        T X,
        T Y,
        T Width,
        T Height
    );
}