using GameEngine.SharedInterface;
using GameEngine.Utilities;

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

            //come back and adjust map lua
            Console.WriteLine($"Welcome {playerService.CurrentPlayer.Id} with color {playerService.CurrentPlayer.Color}");
        }

        private void initializeLocations()
        {
            var mapLocations = _context?.MapContext?.Data?.Metadata?.Locations;
            if(mapLocations == null) return;

            foreach(var location in mapLocations)
            {
                Log.Info($"Init location: {location.Name}");
                
                _context?.Location.Create(location);
            }
        }

        private void initializeTriggerGroups()
        {
            var triggerGroups = _context?.MapContext?.Data?.TriggerGroups;
            if(triggerGroups == null) return;

            foreach(var group in triggerGroups)
            {
                Log.Info($"Init trigger group: {group.Name}");

                _context?.TriggerEngine.AddTriggerGroup(group);
            }
        }

    }
}