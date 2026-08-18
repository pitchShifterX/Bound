using GameEngine.UI.Elements;
using GameEngine.UI.Elements.Editor;
using GameEngine.UI.Properties;
using Mods.Bound.MapEditor;
using Mods.Bound.UI.Elements;
using Mods.Bound.UI.Themes;

namespace Mods.Bound.Scenes.MapEditor
{
    public partial class MapEditorScene
    {
        public override MapEditorTheme UITheme => new();

        public override void BuildUI()
        {
            _editorCanvas = new EditorCanvas(new Fill(), new Fixed(800));

            _mapEditor = new BoundMapEditor(Context, _editorCanvas);
            _mapEditor.Start();

            var menuBar = buildMenuBar();
            var body = buildBody();
            var bottomBar = buildBottomBar();

            UI.Root.AddChild(menuBar);
            UI.Root.AddChild(body);
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

        private UIFlexBox buildBody()
        {
            var container = new UIFlexBox(new Fill(), new Fill())
                .SetGap(10)
                .SetPadding(UISpacing.All(10));

            var leftPanel = buildToolbox();
            
            var rightPanel = new UIFlexBox(new Fill(), new Fill())
                .SetBackgroundColor(UITheme.PrimaryBackground)
                .SetDirection(FlexDirection.Column)
                .SetGap(30);
            
            rightPanel.AddChild(_editorCanvas!);
            rightPanel.AddChild(buildUtilityBar());

            container.AddChild(leftPanel);
            container.AddChild(rightPanel);

            return container;
        }

        private UIFlexBox buildToolbox()
        {
            var container = new UIFlexBox(new Fixed(306), new Fill())
                .SetBackgroundColor(UITheme.SecondaryBackground)
                .SetDirection(FlexDirection.Column)
                .SetGap(10)
                .SetPadding(UISpacing.All(10));

            var minimap = new UIFlexBox(new Fill(), new Fixed(266));

            var tools = new EditorToolbox(_mapEditor!.Context, new Fill(), new Fill())
                .SetDirection(FlexDirection.Row)
                .SetGap(10)
                .SetWrap(FlexWrap.Wrap);

            container.AddChild(minimap);
            container.AddChild(tools);

            return container;
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
                .SetJustifyContent(FlexJustify.Start)
                .SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Center)
                .SetAlignItems(FlexAlign.Center)
                .SetBackgroundColor(UITheme.PrimaryBackground)
                .SetGap(20)
                .SetPadding(UISpacing.All(10));

            var mapSize = new UIText("Map Size 64x64");
            var tileCoords = new EditorTileCoordinatesText();
            var timer = new WorldTimer();

            bottomBar.AddChild(mapSize);
            bottomBar.AddChild(tileCoords);
            // bottomBar.AddChild(timer);
            
            return bottomBar;
        }
    }
}