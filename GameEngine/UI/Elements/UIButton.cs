using GameEngine.Graphics.Primitives;
using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIButton : AbstractContainerElement<UIButton>
    {
        public Action? Action { get; private set; }

        public bool IsHovered { get; private set; }
        public bool IsPressed { get; private set; }

        public Font? Font { get; set; }
        public Color? LabelColor { get; set; }

        public UIButton(UISize width, UISize height, string? label = null, Action? action = null) :
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
            BackgroundColor ??= Context!.Theme.Buttons.BackgroundColor;
            BorderColor ??= Context!.Theme.Buttons.BorderColor;
        }

        public UIButton SetLabel(string label)
        {
            AddChild(new UIText(label)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });

            return Self;
        }

        public UIButton SetLabelColor(Color color)
        {
            LabelColor = color;
            
            return Self;
        }

        public UIButton SetImage(Texture texture, Rectangle<float>? source = null)
        {
            AddChild(
                new UIImage(texture, new Fill(), new Fill(), source)
            );

            return Self;
        }

        public UIButton SetAction(Action action)
        {
            Action = action;

            return Self;
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

            Color? border = BorderColor.HasValue ? BorderColor.Value : null;

            Context.Render.DrawRectangle(Bounds, BackgroundColor!.Value, border);

            base.Render();
        }
    }
}