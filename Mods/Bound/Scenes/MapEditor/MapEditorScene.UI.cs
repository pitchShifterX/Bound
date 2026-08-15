using GameEngine.Graphics.Primitives;
using GameEngine.Resources;
using GameEngine.UI;
using GameEngine.UI.Elements;
using GameEngine.UI.Properties;
using Mods.Bound.UI.Elements;
using Mods.Bound.UI.Themes;

namespace Mods.Bound.Scenes.MapEditor
{
    public partial class MapEditorScene
    {
        public override MapEditorTheme UITheme => new();

        public override void BuildUI()
        {
            var menuBar = buildMenuBar();
            var container = buildContainer();
            var bottomBar = buildBottomBar();

            UI.Root.AddChild(menuBar);
            UI.Root.AddChild(container);
            UI.Root.AddChild(bottomBar);
        }

        private UIFlexBox buildMenuBar()
        {
            var menuBar = new UIFlexBox(new Fill(), new Fixed(50))
                .SetJustifyContent(FlexJustify.Start)
                .SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Center)
                .SetAlignItems(FlexAlign.Center)
                .SetBackgroundColor(UITheme.PrimaryBackground)
                .SetGap(20)
                .SetPadding(UISpacing.All(10));
            
            var menuBarFile = new UIText("New");
            var menuBarOpen = new UIFileOpenButton("lua")
                .SetAction((filter) => _mapEditor?.OnOpenFile(filter));
            var menuBarSave = new UIText("Save...");
            var menuBarSounds = new UIText("Sounds...");
            var menuBarTriggers = new UIText("Triggers...");

            menuBar.AddChild(menuBarFile);
            menuBar.AddChild(menuBarOpen);
            menuBar.AddChild(menuBarSave);
            menuBar.AddChild(menuBarSounds);
            menuBar.AddChild(menuBarTriggers);

            return menuBar;
        }

        private UIFlexBox buildContainer()
        {
            var container = new UIFlexBox(new Fill(), new Fill())
                .SetGap(10)
                .SetPadding(UISpacing.All(10));

            var leftPanel = buildToolbox();
            
            var rightPanel = new UIFlexBox(new Fill(), new Fill())
                .SetBackgroundColor(UITheme.PrimaryBackground)
                .SetDirection(FlexDirection.Column)
                .SetGap(30);
            
            rightPanel.AddChild(buildMapCanvas());
            rightPanel.AddChild(buildUtilityBar());

            container.AddChild(leftPanel);
            container.AddChild(rightPanel);

            return container;
        }

        private UIFlexBox buildToolbox()
        {
            var toolBox = new UIFlexBox(new Fixed(300), new Fill())
                .SetBackgroundColor(UITheme.SecondaryBackground)
                .SetDirection(FlexDirection.Column)
                .SetGap(10)
                .SetPadding(UISpacing.All(10));

            var minimap = new UIFlexBox(new Fill(), new Fixed(250));

            var tools = new UIFlexBox(new Fill(), new Fill());

            toolBox.AddChild(minimap);
            toolBox.AddChild(tools);

            return toolBox;
        }

        private UIFlexBox buildMapCanvas()
        {
            var mapCanvas = new UIFlexBox(new Fill(), new Fixed(800));

            return mapCanvas;
        }

        private UIFlexBox buildUtilityBar()
        {
            var utilityBar = new UIFlexBox(new Fill(), new Fill())
                .SetBackgroundColor(UITheme.PrimaryBackground);

            return utilityBar;
        }

        private UIFlexBox buildBottomBar()
        {
            var bottomBar = new UIFlexBox(new Fill(), new Fixed(30))
                .SetJustifyContent(FlexJustify.SpaceBetween)
                .SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Center)
                .SetAlignItems(FlexAlign.Center)
                .SetBackgroundColor(UITheme.PrimaryBackground)
                .SetGap(20)
                .SetPadding(UISpacing.All(10));

            var mapSize = new UIText("Map Size 64x64");
            var timer = new WorldTimer();

            bottomBar.AddChild(mapSize);
            bottomBar.AddChild(timer);
            
            return bottomBar;
        }
    }
}