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
            initializeLocations();
            initializeTriggerGroups();
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