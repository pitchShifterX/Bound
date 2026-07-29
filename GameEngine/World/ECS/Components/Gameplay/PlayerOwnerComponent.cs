namespace GameEngine.World.ECS.Components.Gameplay
{
    public struct PlayerOwnerComponent
    {
        public string PlayerOwnerId;

        public PlayerOwnerComponent(string playerId)
        {
            PlayerOwnerId = playerId;
        }
    }
}