using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIImage : AbstractContainerElement<UIImage>
    {
        private readonly Texture _resource;

        public UIImage(Texture texture, UISize? width = null, UISize? height = null) : 
            base(width, height)
        {
            _resource = texture;
        }

        public override bool Process(UIInput input)
        {
            foreach(var child in Children)
                child.Process(input);

            return false;
        }

        public override void Layout()
        {
            CalculateBounds();

            LayoutChildren();
        }

        public override void LayoutChildren()
        {
            foreach(var child in Children)
            {
                child.Layout(Bounds);
            }
        }

        public override void Render()
        {
            if(_resource != null)
            {
                Context?.Render.DrawTexture(
                    _resource,
                    null,
                    Bounds
                );
            }

            base.Render();
        }

        public override void Update(float? delta)
        {
            base.Update(delta);
        }
    }
}