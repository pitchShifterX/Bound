using GameEngine.Utilities;

namespace GameEngine.World.ECS.Components.Gameplay
{
    public enum UnitOrderType
    {
        Move
    }

    public struct UnitOrderComponent
    {
        public UnitOrderType Type;
        public Vector2<float> Destination;
    }
}