using GameEngine.Utilities;

namespace GameEngine.MapEditor.Input
{
    public readonly record struct EditorInput(
        Vector2<int> MouseScreenPosition,
        Vector2<int> MouseWorldPosition,
        Vector2<int> MouseTilePosition,
        bool LeftPressed,
        bool LeftDown
    );
}