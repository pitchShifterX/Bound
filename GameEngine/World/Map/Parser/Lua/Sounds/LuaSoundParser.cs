using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua.Sounds
{
    public class LuaSoundParser : ILuaParseTable<List<string>>
    {
        public List<string> Parse(Table table)
        {
            return new LuaTableReader(table)
                .Select((_, sound) => sound.String)
                .ToList();
        }
    }
}