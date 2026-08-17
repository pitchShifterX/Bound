using GameEngine.MapEditor.Input;

namespace GameEngine.MapEditor.Tools
{
    public interface IEditorTool
    {
        public void Process(EditorContext context, EditorInput input);
    }
}