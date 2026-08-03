using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua.Tiles
{
    public class LuaTilesetsParser : ILuaParseTable<List<string>>
    {
        public List<string> Parse(Table table)
        {
            return new LuaTableReader(table)
                .Select((_, tileset) => tileset.String)
                .ToList();
        }
    }
}