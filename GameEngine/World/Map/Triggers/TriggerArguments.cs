using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Triggers
{
    public class TriggerArguments
    {
        private readonly CallbackArguments _args;

        public DynValue this[int index]
        {
            get
            {
                return _args[index];
            }
        }

        public int Count
        {
            get
            {
                return _args.Count;
            }
        }

        public TriggerArguments(CallbackArguments args)
        {
            _args = args;
        }

        public int Int(int index)
        {
            return (int)_args[index].Number;
        }

        public float Float(int index)
        {
            return (float)_args[index].Number;
        }

        public string String(int index)
        {
            return _args[index].String;
        }

        public bool Bool(int index)
        {
            return _args[index].Boolean;
        }
    }
}