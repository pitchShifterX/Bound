using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.World.Map.Tiles;
using Mods.Bound.Gameplay.Sounds;
using Mods.Bound.Gameplay.Triggers.Actions;
using Mods.Bound.Gameplay.Unit;
using Mods.Bound.Scenes.MainMenu;

namespace Mods.Bound
{
    public class BoundMod : Mod<BoundModConfiguration>
    {
        public BoundMod() : base(new())
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var fontPath = Context.Paths?.GetAssetPath("fonts/Inter24Regular.ttf");

            Context.ResourceManager?.Load<Font>("default", fontPath!);
            Context.SceneManager?.Push(() => new MainMenuScene(Context));
        }

        public override void RegisterModContent()
        {
            base.RegisterModContent();

            if(Context.GameRegistries == null) return;

            Context.GameRegistries.UnitPrefab.Register(new RunnerPrefab());
            Context.GameRegistries.UnitPrefab.Register(new ExplosionPrefab());

            Context.GameRegistries.Sounds.Register(new ZombifiedSound());

            Context.GameRegistries.Tilesets.Register(new TilesetDefinition
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

            Context.GameRegistries.Tilesets.Register(new TilesetDefinition
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

            Context.GameRegistries.Triggers.RegisterAction(
                "SetScore",
                args => new SetScoreAction(
                    args.Int(0),
                    args.Int(1),
                    args.String(2)
                )
            );
        }
    }
}