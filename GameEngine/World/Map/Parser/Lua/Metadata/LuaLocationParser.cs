using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;
using GameEngine.World.Map.Locations;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua
{
    public class LuaLocationParser : ILuaParseTable<List<Location>>
    {
        public List<Location> Parse(Table table)
        {
            if(table == null) return [];

            return new LuaTableReader(table)
                .Select((name, location) => new Location
                {
                    Name = location.String("name"),
                    Tiles = parseRectangle(location.Table("tiles")),
                    Color = parseColor(location.String("color"))
                })
                .ToList();
        }

        private Rectangle<int> parseRectangle(Table table)
        {
            var lua = new LuaTableReader(table);

            return new Rectangle<int>
            {
                X = lua.Int("x"),
                Y = lua.Int("y"),
                Width = lua.Int("w"),
                Height = lua.Int("h")
            };
        }

        private Color parseColor(string color)
            => Color.FromString(color);
    }
}