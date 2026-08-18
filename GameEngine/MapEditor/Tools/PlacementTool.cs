using GameEngine.MapEditor.Input;
using GameEngine.UI.Event.Types;
using GameEngine.Utilities;

namespace GameEngine.MapEditor.Tools
{
    public abstract class PlacementTool : IEditorTool
    {
        public abstract void Place(EditorContext context, Vector2<int> position);
        public abstract void Process(EditorContext context, EditorInput input);
    }
}