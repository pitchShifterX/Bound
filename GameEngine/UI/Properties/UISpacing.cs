namespace GameEngine.UI.Properties
{
    public record struct UISpacing(
        float Top,
        float Right,
        float Bottom,
        float Left
    )
    {
        public static UISpacing Zero => new();

        public static UISpacing All(float x)
        {
            return new(x, x, x, x);
        }
    }
}