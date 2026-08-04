using GameEngine.World.Map.Locations;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Player;

namespace GameEngine.World.Map
{
    public class MapInitializer
    {
        private readonly PlayerService _players;
        private readonly LocationService _locations;
        private readonly TriggerEngine _triggers;

        public MapInitializer(
            PlayerService players,
            LocationService locations,
            TriggerEngine triggerEngine
        )
        {
            _players = players;
            _locations = locations;
            _triggers = triggerEngine;
        }

        public void Initialize(MapData map)
        {
            initializePlayers(map);
            initializeLocations(map);
            initializeTriggerGroups(map);
        }

        private void initializePlayers(MapData map)
        {
            if(_players == null || map.Metadata?.Players == null) return;

            foreach(var player in map.Metadata.Players)
            {
                _players.RegisterPlayer(player);
            }

            var firstHumanSlot = map.Metadata.Players.First(x => x.IsHuman);
            
            _players.SetLocalPlayer(firstHumanSlot.Id);
        }

        private void initializeLocations(MapData map)
        {
            var mapLocations = map.Metadata?.Locations;
            if(mapLocations == null) return;

            foreach(var location in mapLocations)
            {
                _locations.Create(location);
            }
        }

        private void initializeTriggerGroups(MapData map)
        {
            var triggerGroups = map.TriggerGroups;
            if(triggerGroups == null) return;

            foreach(var group in triggerGroups)
            {
                _triggers.AddTriggerGroup(group);
            }
        }

    }
}