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
    }
}