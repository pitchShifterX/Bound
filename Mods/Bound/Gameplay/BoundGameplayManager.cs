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
            SceneContext.Load<Texture>("dirt", "textures/dirt.png");
            SceneContext.Load<Texture>("runner", "textures/runner.png");

            GameplayContext?.MapContext?.LoadMap("TestMap.lua");

            base.Load();
        }

        protected override void RegisterModContent()
        {
            Registries.UnitPrefab.Register(new BounderPrefab());
        }
    }
}