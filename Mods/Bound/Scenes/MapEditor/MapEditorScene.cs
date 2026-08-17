using GameEngine.Event.Input;
using GameEngine.MapEditor;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.UI.Elements.Editor;

namespace Mods.Bound.Scenes.MapEditor
{
    public partial class MapEditorScene(IModContext modContext)
        : Scene(modContext)
    {
        private Editor? _mapEditor;
        private EditorCanvas? _editorCanvas;

        public override void Load()
        {
            Context.LoadFont("normal", "fonts/Inter24Regular.ttf", 16);
            Context.GetById<Font>("normal");
        }

        public override void ProcessInput(IRecordInput input)
        {
            _mapEditor?.Process(input);
            UI.Process(input);
        }

        public override void Render()
        {
            UI.Render();
            _mapEditor?.Render();
        }

        public override void Update(float? delta)
        {
            _mapEditor?.Update(delta);
            UI.Update(delta);
        }
    }
}