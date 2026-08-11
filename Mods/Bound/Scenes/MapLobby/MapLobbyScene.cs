using GameEngine;
using GameEngine.Event.Input;
using GameEngine.Graphics;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.SharedInterface;
using Mods.Bound.Gameplay;
using Mods.Bound.Scenes.MainMenu;

namespace Mods.Bound.Scenes.Gameplay
{
    public partial class MapLobbyScene(IModContext modContext)
        : Scene(modContext)
    {
        private Font? _default;
        private Font? _interExtraLight;

        public override void Load()
        {
            _default = Context.GetById<Font>("default");

            Context.LoadFont("interextralight@16", "fonts/Inter18ExtraLight.ttf", 16);
            _interExtraLight = Context.GetById<Font>("interextralight@16");
        }

        public override void ProcessInput(IRecordInput input)
        {
            if(input.WasKeyPressed(KeyCode.Escape))
            {
                Context.ReplaceScene(() => new MainMenuScene(ModContext));

                return;
            }
        }

        public override void Render()
        {
            UI.Render();
        }

        public override void Update(float? delta)
        {
        }
    }
}