using GameEngine.UI.Input;
using GameEngine.UI.Properties;

namespace GameEngine.UI.Elements.Editor
{
    public class EditorCanvas : UIFlexBox, IEditorViewport
    {
        public EditorCanvas(UISize width, UISize height) : base(width, height)
        {
            
        }

        public override bool Process(UIInput input)
        {
            return false;
        }
    }
}