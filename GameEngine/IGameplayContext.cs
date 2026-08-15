using GameEngine.World;
using GameEngine.World.Input.Commands;

namespace GameEngine
{
    public interface IGameplayContext : IWorldContext
    {
        public CommandService Commands { get; }
    }
}