using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIImage : AbstractContainerElement<UIImage>
    {
        private readonly Texture _resource;

        public Rectangle<float>? SourceRectangle { get; set; }

        public UIImage(
            Texture texture, 
            UISize? width = null, 
            UISize? height = null,
            Rectangle<float>? source = null
        ) : base(width, height)
        {
            _resource = texture;
            SourceRectangle = source;
        }

        public UIImage SetSource(Rectangle<float> source)
        {
            SourceRectangle = source;

            return Self;
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
                    SourceRectangle,
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