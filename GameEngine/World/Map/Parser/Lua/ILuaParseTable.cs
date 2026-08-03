using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua
{
    public interface ILuaParseTable<T>
    {
        T Parse(Table table);
    }
}