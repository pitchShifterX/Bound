using GameEngine.Graphics.Primitives;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;

namespace GameEngine.UI.Elements
{
    public class UIPanel : UIElement
    {
        public Color BorderColor { get; set; } = Color.White;

        public UIPanel(){}

        public UIPanel(UISize width, UISize height) :
            base(width, height)
        {
            
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
            foreach(var child in Children)
                child.Render();
        }
    }
}