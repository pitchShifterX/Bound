using GameEngine;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.World.Map.Tiles;
using Mods.Bound.Gameplay.Sounds;
using Mods.Bound.Gameplay.Triggers.Actions;
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
    }
}