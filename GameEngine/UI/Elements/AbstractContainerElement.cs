using GameEngine.Graphics.Primitives;
using GameEngine.UI.Properties;

namespace GameEngine.UI.Elements
{
    public abstract class AbstractContainerElement<T> : UIElement
        where T : AbstractContainerElement<T>
    {
        public Color? BorderColor { get; set; }

        public AbstractContainerElement(){}
        public AbstractContainerElement(UISize width, UISize height) : 
            base(width, height)
        {
            
        }

        public T SetMargin(UISpacing spacing)
        {
            Margin = spacing;

            return (T)this;
        }

        public T SetPadding(UISpacing spacing)
        {
            Padding = spacing;

            return (T)this;
        }

        public T SetBorderColor(Color color)
        {
            BorderColor = color;

            return (T)this;
        }
    }
}