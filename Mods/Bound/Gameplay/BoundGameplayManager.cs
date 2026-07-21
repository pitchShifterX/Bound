using GameEngine;
using GameEngine.Resources;
using GameEngine.Scene;

namespace Mods.Bound.Gameplay
{
    public class BoundGameplayManager : GameplayManager
    {
        public BoundGameplayManager(ISceneContext context)
            : base(context)
        {
            
        }

        public override void Load()
        {
            loadDefaultTiles();
            
            MapContext.LoadMap("TestMap.lua");

            base.Load();
        }

        private void loadDefaultTiles()
        {
            SceneContext.Load<Texture>("dirt", "textures/dirt.png");
        }
    }
}