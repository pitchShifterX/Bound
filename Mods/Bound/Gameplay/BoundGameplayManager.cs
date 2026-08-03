using GameEngine;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.World.Map.Tiles;
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
            // SceneContext.Load<Texture>("dirt", "textures/dirt.png");
            SceneContext.Load<Texture>("runner", "textures/runner.png");

            GameplayContext?.MapContext?.LoadMap("TestMap.lua");

            base.Load();
        }

        protected override void RegisterModContent()
        {
            base.RegisterModContent();
            
            Registries.UnitPrefab.Register(new BounderPrefab());

            Registries.Tilesets.Register(new TilesetDefinition
            {
                Id = "dirt",
                TexturePath = "textures/dirt.png",
                Columns = 3,
                Rows = 3
            });

            Registries.Tilesets.Register(new TilesetDefinition
            {
                Id = "water",
                TexturePath = "textures/water.png",
                Columns = 3,
                Rows = 3
            });
        }
    }
}