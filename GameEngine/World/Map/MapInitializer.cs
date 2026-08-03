using GameEngine.SharedInterface;

namespace GameEngine.World.Map
{
    public class MapInitializer : IInitializable
    {
        private IGameplayContext _context;

        public MapInitializer(IGameplayContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            initializePlayers();
            initializeLocations();
            initializeTriggerGroups();
        }

        private void initializePlayers()
        {
            var playerService = _context?.Player;
            var players = _context?.MapContext?.Data?.Metadata?.Players;
            if(playerService == null || players == null) return;

            foreach(var player in players)
            {
                playerService.RegisterPlayer(player);
            }

            var firstHumanSlot = players.First(x => x.IsHuman);
            
            playerService.SetLocalPlayer(firstHumanSlot.Id);
        }

        private void initializeLocations()
        {
            var mapLocations = _context?.MapContext?.Data?.Metadata?.Locations;
            if(mapLocations == null) return;

            foreach(var location in mapLocations)
            {
                _context?.Location.Create(location);
            }
        }

        private void initializeTriggerGroups()
        {
            var triggerGroups = _context?.MapContext?.Data?.TriggerGroups;
            if(triggerGroups == null) return;

            foreach(var group in triggerGroups)
            {
                _context?.TriggerEngine.AddTriggerGroup(group);
            }
        }

    }
}