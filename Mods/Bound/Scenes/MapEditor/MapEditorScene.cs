using GameEngine.Event.Input;
using GameEngine.MapEditor;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;
using Mods.Bound.MapEditor;

namespace Mods.Bound.Scenes.MapEditor
{
    public partial class MapEditorScene(IModContext modContext)
        : Scene(modContext)
    {
        private Editor? _mapEditor;
        private Font? _smallInter;

        public override void Load()
        {
            _mapEditor = new BoundMapEditor(Context);
            _mapEditor.Start();

            Context.LoadFont("normal", "fonts/Inter24Regular.ttf", 16);
            _smallInter = Context.GetById<Font>("normal");
        }

        public override void ProcessInput(IRecordInput input)
        {
            _mapEditor?.Process(input);
            UI.Process(input);
        }

        public override void Render()
        {
            _mapEditor?.Render();
            UI.Render();
        }

        public override void Update(float? delta)
        {
            _mapEditor?.Update(delta);
            UI.Update(delta);
        }
    }
}