using GameEngine.Graphics.Primitives;
using GameEngine.Graphics.Rendering;
using GameEngine.Graphics.Text;
using GameEngine.Resources;

namespace GameEngine.Scene.UI
{
    public class UIManager : IUIController
    {
        private IRendererController _renderer;
        
        public UIManager(IRendererController renderer)
        {
            _renderer = renderer;
        }

        public void DrawDynamicText(TextData data, string text)
        {
            throw new NotImplementedException();
        }

        public void DrawDynamicText(Font font, string text, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawStaticText(TextData data, string text)
        {
            throw new NotImplementedException();
        }

        public void DrawStaticText(Font font, string text, Color color)
        {
            throw new NotImplementedException();
        }
    }
}