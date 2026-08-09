using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIImage : UIElement
    {
        private readonly Texture _resource;

        public UIImage(Texture texture)
        {
            _resource = texture;
        }

        public override bool Process(UIInput input)
        {
            return false;
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