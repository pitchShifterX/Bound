using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIImage : UIElement
    {
        private readonly Texture _resource;

        public UIImage(Texture texture) :
            base(new Rectangle<float>
            {
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0
            })
        {
            _resource = texture;

            WidthMode = UISizeMode.Fill;
            HeightMode = UISizeMode.Fill;
        }

        public UIImage(Rectangle<float> rect, Texture texture) : 
            base(rect)
        {
            _resource = texture;

            Rectangle = rect;
        }

        public override bool Process(UIInput input)
        {
            return true;
        }

        public override void Render()
        {
            if(_resource == null) return;

            Context?.Render.DrawTexture(_resource, null, Bounds);
        }

        public override void Update(float? delta)
        {
            base.Update(delta);
        }
    }
}