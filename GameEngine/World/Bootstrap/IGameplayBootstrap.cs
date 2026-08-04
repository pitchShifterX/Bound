using GameEngine.World.Assets;
using GameEngine.World.Map;

namespace GameEngine.World.Bootstrap
{
    public interface IGameplayBootstrap
    {
        public IMapContext MapContext { get; }
        public AssetLoader AssetLoader { get; }
        public MapInitializer MapInitializer { get; }

        public void Validate();
        public void LoadMap(string fileName);
        public void Initialize();
    }
}