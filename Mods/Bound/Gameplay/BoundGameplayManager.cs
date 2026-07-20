using GameEngine;
using GameEngine.Scene;
using GameEngine.World.Map;

namespace Mods.Bound.Gameplay
{
    public class BoundGameplayManager : GameplayManager
    {
        private MapLuaLoader _mapLoader = new MapLuaLoader();

        public BoundGameplayManager(ISceneContext context)
            : base(context)
        {
            
        }

        public override void Load()
        {
            var mapPath = Context.Paths.GetMapsPath("TestMap.lua");
            var mapData = _mapLoader.Load(mapPath);

            Console.WriteLine(mapData.ToString());

            base.Load();
        }
    }
}