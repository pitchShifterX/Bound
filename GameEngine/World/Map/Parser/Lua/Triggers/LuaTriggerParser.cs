using GameEngine.World.Map.Triggers;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Parser.Lua.Triggers
{
    public class LuaTriggerParser : ILuaParseTable<List<Trigger>>
    {
        public List<Trigger> Parse(Table table)
        {
            var triggerList = new List<Trigger>();

            if(table == null)
                return triggerList;
            
            foreach(var pair in table.Pairs)
            {
                var triggerTable = new LuaTableReader(pair.Value.Table);
                if(triggerTable == null) continue;

                var trigger = new Trigger
                {
                    Name = triggerTable.String("name"),
                    IsPreserved = triggerTable.Boolean("preserved")
                };

                var conditions = triggerTable.Table("conditions");
                var actions = triggerTable.Table("actions");

                trigger.Conditions.AddRange(parseConditions(conditions));
                trigger.Actions.AddRange(parseActions(actions));

                triggerList.Add(trigger);
            }

            return triggerList;
        }

        private List<ITriggerCondition> parseConditions(Table table)
        {
            var conditionList = new List<ITriggerCondition>();

            if(table == null)
                return conditionList;
            
            foreach(var pair in table.Pairs)
            {
                var condition = pair.Value.ToObject<ITriggerCondition>();

                if(condition != null)
                    conditionList.Add(condition);
            }

            return conditionList;
        }

        private List<ITriggerAction> parseActions(Table table)
        {
            var actionList = new List<ITriggerAction>();

            if(table == null)
                return actionList;
            
            foreach(var pair in table.Pairs)
            {
                var action = pair.Value.ToObject<ITriggerAction>();

                if(action != null)
                    actionList.Add(action);
            }

            return actionList;
        }
    }
}