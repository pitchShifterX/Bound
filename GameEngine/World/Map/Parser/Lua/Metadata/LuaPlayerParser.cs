using GameEngine.Exception;
using GameEngine.World.Player;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua.Metadata
{
    public class LuaPlayerParser : ILuaParseTable<List<PlayerData>>
    {
        public List<PlayerData> Parse(Table table)
        {
            if(table == null)
                throw new MapPlayersUndefinedException($"Players undefined in map.");
            
            return new LuaTableReader(table)
                .Select((name, player) => new PlayerData
                {
                    Name = name,
                    Id = player.Int("id"),
                    Color = player.String("color"),
                    IsHuman = player.Boolean("human")
                })
                .ToList();
        }
    }
}