namespace GameEngine.UI.Properties
{
    public abstract record UISize;
    public sealed record Fixed(float Value) : UISize;
    public sealed record Fill : UISize;
    public sealed record Auto : UISize;
}