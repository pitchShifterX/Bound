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
            if(!_registeredPlayers.TryGetValue(id, out var player))
            {
                throw new InvalidOperationException($"Player id {id} not registered.");
            }

            return player;
        }

        public void SetCustomVariableForPlayer(int playerId, string key, object value)
        {
            var player = GetPlayer(playerId);

            player.CustomVariables[key] = value;
        }

        public bool IsCustomVariableForPlayer(int playerId, string key)
        {
            var player = GetPlayer(playerId);

            return player.CustomVariables.ContainsKey(key);
        }

        public bool TryGetCustomVariableValueForPlayer(
            int playerId, 
            string key, 
            out object? value
        )
        {
            var player = GetPlayer(playerId);

            return player.CustomVariables.TryGetValue(key, out value);
        }
    }
}