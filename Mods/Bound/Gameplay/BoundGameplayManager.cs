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

        public override void Start()
        {
            GameplayContext?.LoadMap("TestMap.lua");
        }

        protected override void RegisterModContent()
        {
            base.RegisterModContent();

            SceneContext.Load<Texture>("runner", "textures/runner.png");
            
            Registries.UnitPrefab.Register(new BounderPrefab());

            Registries.Tilesets.Register(new TilesetDefinition
            {
                Id = "dirt",
                TexturePath = "textures/dirt.png",
                Columns = 3,
                Rows = 3,
                TileDefinitions =
                [
                    new(){ IsWalkable = true },
                    new(){ IsWalkable = true },
                    new(){ IsWalkable = true },
                    new(){ IsWalkable = true },
                    new(){ IsWalkable = true },
                    new(){ IsWalkable = true },
                    new(){ IsWalkable = true },
                    new(){ IsWalkable = true },
                    new(){ IsWalkable = true },
                ]
            });

            Registries.Tilesets.Register(new TilesetDefinition
            {
                Id = "water",
                TexturePath = "textures/water.png",
                Columns = 3,
                Rows = 3,
                TileDefinitions =
                [
                    new(){ IsWalkable = false },
                    new(){ IsWalkable = false },
                    new(){ IsWalkable = false },
                    new(){ IsWalkable = false },
                    new(){ IsWalkable = false },
                    new(){ IsWalkable = false },
                    new(){ IsWalkable = false },
                    new(){ IsWalkable = false },
                    new(){ IsWalkable = false },
                ]
            });
        }
    }
}