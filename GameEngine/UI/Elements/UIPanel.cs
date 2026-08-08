using GameEngine.Graphics.Primitives;
using GameEngine.UI.Input;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIPanel : UIElement
    {
        public UIPanel(Rectangle<float> rect) :
            base(rect)
        {
            Rectangle = rect;
        }

        public override bool Process(UIInput input)
        {
            // process in reverse order (last element added in)
            for(int i = Children.Count - 1; i >= 0; i--)
            {
                if(Children[i].Process(input))
                    return true;
            }

            return false;
        }

        public override void Update(float? delta)
        {
            base.Update(delta);
            
            foreach(var child in Children)
                child.Update(delta);
        }

        public override void Render()
        {
            Context?.Render.DrawRectangle(Rectangle, Color.White);
            foreach(var child in Children)
                child.Render();
        }
    }
}