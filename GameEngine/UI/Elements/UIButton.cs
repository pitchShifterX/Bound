using GameEngine.Graphics.Primitives;
using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;

namespace GameEngine.UI.Elements
{
    public class UIButton : UIElement
    {
        public Action? Action { get; private set; }

        public bool IsHovered { get; private set; }
        public bool IsPressed { get; private set; }

        public Font? Font { get; set; }
        public Color? LabelColor { get; set; }
        public Color? BorderColor { get; set; }

        public UIButton(UISize width, UISize height, string? label, Action? action) :
            base(width, height)
        {
            if(label != null)
                SetLabel(label);
            
            if(action != null)
                SetAction(action);
        }

        protected override void OnContextAssigned()
        {
            Font ??= Context!.Scene.GetById<Font>(Context!.Theme.FontResource);
            LabelColor ??= Context!.Theme.Buttons.LabelColor;
            BorderColor ??= Context!.Theme.Buttons.BorderColor;
        }

        public void SetLabel(string label)
        {
            AddChild(new UIText(label)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });
        }

        public UIButton SetAction(Action action)
        {
            Action = action;

            return this;
        }

        public override bool Process(UIInput input)
        {
            IsHovered = Bounds.Contains(input.MousePosition);

            if(!IsHovered)
                return false;

            if(input.MouseLeftPressed)
            {
                IsPressed = true;

                return true;
            }
            
            if(IsPressed && input.MouseLeftReleased)
            {
                IsPressed = false;
                Action?.Invoke();

                return true;
            }

            return true;
        }

        public override void Update(float? delta)
        {
            base.Update(delta);
        }

        public override void Render()
        {
            if(Context?.Render == null) return;

            Context.Render.DrawRectangle(Bounds, BorderColor!.Value);

            base.Render();
        }
    }
}