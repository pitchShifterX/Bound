using GameEngine.Graphics.Primitives;
using GameEngine.UI.Properties;

namespace GameEngine.UI.Elements
{
    public abstract class AbstractContainerElement<T> : UIElement<T>
        where T : AbstractContainerElement<T>
    {
        public Color? BackgroundColor { get; set; }
        public Color? BorderColor { get; set; }

        public AbstractContainerElement(){}
        public AbstractContainerElement(UISize width, UISize height) : 
            base(width, height)
        {
            
        }

        public T SetMargin(UISpacing spacing)
        {
            Margin = spacing;

            return Self;
        }

        public T SetPadding(UISpacing spacing)
        {
            Padding = spacing;

            return Self;
        }

        public T SetBackgroundColor(Color color)
        {
            BackgroundColor = color;

            return Self;
        }

        public T SetBorderColor(Color color)
        {
            BorderColor = color;

            return Self;
        }
    }
}