namespace GameEngine.World.ECS.Components.Gameplay
{
    public struct PlayerOwnerComponent
    {
        public int PlayerOwnerId;

        public PlayerOwnerComponent(int playerId)
        {
            PlayerOwnerId = playerId;
        }
    }
}