using GameEngine;
using GameEngine.Resources;
using GameEngine.Scene;
using Mods.Bound.Gameplay.Unit;

namespace Mods.Bound.Gameplay
{
    public sealed class BoundGameplayManager : GameplayManager
    {
        public BoundGameplayManager(ISceneContext context)
            : base(context)
        {
            
        }

        public override void Load()
        {
            MapContext.LoadMap("TestMap.lua");

            base.Load();
        }

        protected override void RegisterModContent()
        {
            SceneContext.Load<Texture>("dirt", "textures/dirt.png");
            SceneContext.Load<Texture>("runner", "textures/runner.png");
            UnitRegistry.Register(new BounderPrefab());
        }
    }
}