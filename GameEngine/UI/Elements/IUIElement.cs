using GameEngine.SharedInterface;
using GameEngine.UI.Input;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public interface IUIElement : IUpdatable
    {
        public IUIElement? Parent { get; set; }
        public bool IsEnabled { get; }
        public bool IsVisible { get; }
        public Rectangle<float> Rectangle { get; }
        public Rectangle<float> Bounds { get; }
        public Vector2<float> Center { get; }
        public void SetContext(UIContext context);
        public void Layout();
        public void AddChild(IUIElement element);
        public void AddChildren(IUIElement[] elements);
        public bool Process(UIInput input);
        public void Render();
    }
}