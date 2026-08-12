using GameEngine.Event.Input;
using GameEngine.Scene;
using GameEngine.SharedInterface;
using GameEngine.UI.Elements;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;
using GameEngine.World.Input;

namespace GameEngine.UI
{
    public class UIManager : IUpdatable, IRenderable, IProcessInput
    {
        private readonly ISceneContext _sceneContext;
        private Vector2<float> _virtualResolution = new(1920, 1080);
        private Vector2<float> _screenResolution;

        public UIContext Context { get; private set; }

        public AbstractContainerElement<UIFlexBox> Root { get; private set; }

        public UIManager(ISceneContext sceneContext, IUITheme theme)
        {
            _sceneContext = sceneContext;
            _screenResolution = _sceneContext.Settings.WindowSize.To<float>();

            var renderContext = createRenderContext();

            Context = new UIContext(
                _sceneContext,
                renderContext,
                theme
            );

            Root = new UIFlexBox(
                new Fixed(_virtualResolution.x),
                new Fixed(_virtualResolution.y)
            ).SetDirection(FlexDirection.Column);
            
            Root.SetContext(Context);
        }

        public void Layout()
        {
            Root.Layout(new Rectangle<float>
            {
                X = 0,
                Y = 0,
                Width = _virtualResolution.x,
                Height = _virtualResolution.y
            });
        }

        public void Process(IRecordInput input)
        {
            // need to refactor this into a transform obj
            var mousePosition = Context.Render.ScreenToUI(
                input.MousePositionX,
                input.MousePositionY
            );

            var uiInput = new UIInput(
                mousePosition,
                input.WasMouseButtonPressed(MouseButton.Left),
                input.WasMouseButtonReleased(MouseButton.Left),
                input.IsMouseButtonPressed(MouseButton.Left)
            );

            Root.Process(uiInput);
        }

        public void Update(float? delta)
        {
            Root.Update(delta);
        }

        public void Render()
        {
            Root.Render();
        }

        private UIRenderContext createRenderContext()
        {
            var scaleX = (float)_screenResolution.x / _virtualResolution.x;
            var scaleY = (float)_screenResolution.y / _virtualResolution.y;
            var scale = MathF.Min(scaleX, scaleY);

            var offsetX = (_screenResolution.x - _virtualResolution.x * scale) / 2f;
            var offsetY = (_screenResolution.y - _virtualResolution.y * scale) / 2f;

            return new UIRenderContext(
                _sceneContext,
                scale, 
                offsetX, 
                offsetY
            );
        }
    }
}