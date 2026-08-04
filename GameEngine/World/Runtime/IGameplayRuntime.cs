using GameEngine.Scene;
using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.Input;
using GameEngine.World.Input.Commands;
using GameEngine.World.Map;
using GameEngine.World.Player;
using GameEngine.World.Rendering.Cameras;
using GameEngine.World.Time;

namespace GameEngine.World.Runtime
{
    public interface IGameplayRuntime : IUpdatable
    {
        public CameraContext? Camera { get; }
        public SelectionService? Selection { get; }
        public InputService? Input { get; }

        public void Initialize(
            ISceneContext scene, 
            IMapContext map, 
            PlayerService player, 
            CommandService commands,
            ECSService ecs
        );
    }
}