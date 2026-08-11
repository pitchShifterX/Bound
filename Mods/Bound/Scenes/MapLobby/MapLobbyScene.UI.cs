using GameEngine.Graphics.Primitives;
using GameEngine.UI.Elements;
using GameEngine.UI.Properties;

namespace Mods.Bound.Scenes.Gameplay
{
    public partial class MapLobbyScene
    {
        public override void BuildUI()
        {
            var header = buildHeader();
            var body = buildBody();

            UI.Root.AddChild(header);
            UI.Root.AddChild(body);
            UI.Root.SetPadding(new(0, 100, 0, 100));
        }

        private IUIElement buildHeader()
        {
            var header = new UIPanel(new Fill(), new Fixed(150))
                .SetPadding(new(25, 0, 0, 0))
                .SetBackgroundColor(Color.Transparent);

            var title = new UIText("Map Lobby")
                .SetColor(Color.White)
                .SetFont(_default!);

            var subtitle = new UIText("Single Player")
                .SetFont(_interExtraLight!)
                .SetMargin(new(50, 0, 0, 0))
                .SetColor(Color.White);

            header.AddChild(title);
            header.AddChild(subtitle);

            return header;
        }

        private IUIElement buildBody()
        {
            var bodyPanel = new UIPanel(new Fill(), new Fill())
                // .SetBorderColor(Color.Green)
                .SetPadding(new UISpacing(150, 0, 150, 0));
            
            var mapPanel = new UIPanel(new Fixed(500), new Fill())
                .SetBorderColor(new Color(255, 255, 255, 50))
                .SetBackgroundColor(new Color(17, 17, 17, 255));
            
            var rightPanel = new UIPanel(new Fill(), new Fill())
                .SetBorderColor(new Color(255, 255, 255, 50))
                .SetBackgroundColor(new Color(17, 17, 17, 255))
                .SetMargin(new UISpacing(0, 0, 0, 600));
            
            bodyPanel.AddChild(mapPanel);
            bodyPanel.AddChild(rightPanel);

            return bodyPanel;
        }
    }
}