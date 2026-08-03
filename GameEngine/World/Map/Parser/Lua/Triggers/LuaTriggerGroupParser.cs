using GameEngine.World.Map.Triggers;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua.Triggers
{
    public class LuaTriggerGroupParser : ILuaParseTable<List<TriggerGroup>>
    {
        private LuaTriggerParser _trigger = new();

        public List<TriggerGroup> Parse(Table table)
        {
            var groupList = new List<TriggerGroup>();

            if(table == null)
                return groupList;

            var lua = new LuaTableReader(table);
            
            foreach(var pair in lua.Pairs())
            {
                var groupTable = new LuaTableReader(pair.Value.Table);
                if(groupTable == null) continue;

                var group = new TriggerGroup(
                    groupTable.String("name"),
                    groupTable.String("description")
                )
                {
                    IsEnabled = groupTable.Boolean("enabled")
                };

                var triggerTable = groupTable.Table("triggers");
                var triggers = _trigger.Parse(triggerTable);

                group.Triggers.AddRange(triggers);

                groupList.Add(group);
            }

            return groupList;
        }
    }
}