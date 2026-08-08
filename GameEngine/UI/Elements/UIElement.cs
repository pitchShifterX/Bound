using GameEngine.UI.Input;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public enum UISizeMode
    {
        Fixed,
        Fill
    }

    public abstract class UIElement : IUIElement
    {
        public string? Id { get; set; }
        public bool IsEnabled { get; set; } = true;
        public bool IsVisible { get; set; } = true;

        public UISizeMode WidthMode { get; init; } = UISizeMode.Fixed;
        public UISizeMode HeightMode { get; init; } = UISizeMode.Fixed;

        public Rectangle<float> Rectangle { get; set; }

        public IUIElement? Parent { get; set; }
        public List<IUIElement> Children { get; protected set; } = [];

        public Rectangle<float> Bounds { get; protected set; }

        public Vector2<float> Center => new(
            Bounds.X + Bounds.Width / 2,
            Bounds.Y + Bounds.Height / 2
        );

        protected UIContext? Context { get; private set; }

        protected UIElement(Rectangle<float> rectangle)
        {
            Rectangle = rectangle;
            Bounds = rectangle;
        }

        public virtual void SetContext(UIContext context)
        {
            Context = context;

            OnContextAssigned();
        }

        public virtual void Layout()
        {
            CalculateBounds();

            foreach(var child in Children)
                child.Layout();
        }

        public void AddChild(IUIElement element)
        {
            element.Parent = this;

            if(Context != null)
                element.SetContext(Context);

            Children.Add(element);
        }

        public void AddChildren(IUIElement[] elements)
        {
            foreach(var element in elements)
            {
                AddChild(element);
            }
        }

        public virtual void Update(float? delta)
        {
        }

        public abstract bool Process(UIInput input);
        public abstract void Render();
        protected virtual void OnContextAssigned(){}

        protected virtual void CalculateBounds()
        {
            if(Parent == null)
            {
                Bounds = Rectangle;

                return;
            }
            
            var parentBounds = Parent.Bounds;
            var width = Rectangle.Width;
            var height = Rectangle.Height;

            if(WidthMode == UISizeMode.Fill)
                width = MathF.Max(parentBounds.Width - Rectangle.X, 0);

            if(HeightMode == UISizeMode.Fill)
                height = MathF.Max(parentBounds.Height - Rectangle.Y, 0);

            Bounds = new Rectangle<float>
            {
                X = parentBounds.X + Rectangle.X,
                Y = parentBounds.Y + Rectangle.Y,
                Width = width,
                Height = height
            };
        }
    }
}