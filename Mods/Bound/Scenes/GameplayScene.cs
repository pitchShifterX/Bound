using GameEngine;
using GameEngine.Event.Input;
using GameEngine.Graphics;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.Utilities;
using Mods.Bound.Gameplay;
using SDL2;

namespace Mods.Bound.Scenes
{
    public class GameplayScene(IModContext modContext)
        : Scene(modContext)
    {
        private GameplayManager? _gameplay;

        public override void Load()
        {
            _gameplay = new BoundGameplayManager(Context);
            _gameplay.Load();
        }

        public override void ProcessInput(IRecordInput input)
        {
        }

        public override void Render()
        {
        }

        public override void Update(float? delta)
        {
        }
    }
}