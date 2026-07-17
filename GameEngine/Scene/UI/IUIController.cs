using GameEngine.Graphics;
using GameEngine.Resources;

namespace GameEngine.Scene.UI
{
    public interface IUIController
    {
        public void DrawStaticText(TextData data, string text);
        public void DrawStaticText(Font font, string text, Color color);
        public void DrawDynamicText(TextData data, string text);
        public void DrawDynamicText(Font font, string text, Color color);
    }
}