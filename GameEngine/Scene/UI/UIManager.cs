using GameEngine.Graphics;
using GameEngine.Render;
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