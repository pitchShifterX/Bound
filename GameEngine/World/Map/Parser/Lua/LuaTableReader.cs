using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua
{
    public class LuaTableReader
    {
        private readonly Table _table;

        public LuaTableReader(Table table)
        {
            _table = table;
        }

        public string String(string key)
            => _table.Get(key).String;
        
        public int Int(string key)
            => (int)_table.Get(key).Number;

        public bool Boolean(string key)
            => _table.Get(key).Boolean;

        public Table Table(string key)
            => _table.Get(key).Table;

        public IEnumerable<TablePair> Pairs()
            => _table.Pairs;

        public IEnumerable<T> Select<T>(Func<string, LuaTableReader, T> selector)
        {
            foreach (var pair in _table.Pairs)
            {
                if (pair.Value.Type != DataType.Table)
                    continue;

                yield return selector(
                    pair.Key.String,
                    new LuaTableReader(pair.Value.Table)
                );
            }
        }

        public IEnumerable<T> Select<T>(Func<string, DynValue, T> selector)
        {
            foreach (var pair in _table.Pairs)
            {
                yield return selector(pair.Key.String, pair.Value);
            }
        }
    }
}