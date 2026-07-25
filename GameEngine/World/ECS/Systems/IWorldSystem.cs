using GameEngine.SharedInterface;

namespace GameEngine.World.ECS.Systems
{
    public interface IWorldSystem : IInitializable
    {
        public void Update(ECSService service, float? deltaTime);
    }
}