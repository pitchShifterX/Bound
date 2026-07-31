using GameEngine.Utilities;

namespace GameEngine.World.Player
{
    public class PlayerService
    {
        private Dictionary<int, IPlayer> _registeredPlayers = [];

        private int? _currentPlayerId;

        public IPlayer CurrentPlayer
        {
            get
            {
                if(_currentPlayerId == null)
                    throw new NullReferenceException("Current player id not assigned to local player.");

                return _registeredPlayers[_currentPlayerId.Value];
            }
        }

        public void RegisterPlayer(IPlayer player)
        {
            try
            {
                _registeredPlayers.Add(player.Id, player);
            }
            catch(ArgumentException e)
            {
                Log.Error($"Could not add registered player. The map most likely did not set ids for players. Duplicate ids were found. {e}");

                throw;
            }
        }

        public void SetLocalPlayer(int playerId)
        {
            if(!_registeredPlayers.ContainsKey(playerId))
                throw new NullReferenceException("Player id not registered.");

            _currentPlayerId = playerId;
        }

        public IPlayer GetPlayer(int id)
        {
            return _registeredPlayers[id];
        }
    }
}