using GameEngine.Graphics.Primitives;
using GameEngine.UI.Properties;

namespace GameEngine.UI.Elements
{
    public class UIFileOpenButton : UIButton
    {
        public string Filter { get; init; }

        public UIFileOpenButton(string filter) : 
            base(
                new Fixed(50), 
                new Fill(), 
                "Open"
            )
        {
            Filter = filter;
        }

        public UIFileOpenButton SetAction(Action<string> action)
        {
            SetAction(() => action(Filter));
            
            return this;
        }

        protected override void OnContextAssigned()
        {
            base.OnContextAssigned();

            BackgroundColor = Color.Transparent;
            BorderColor = null;
        }
    }
}