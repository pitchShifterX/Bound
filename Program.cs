using GameEngine.Mod;
using Mods.Bound;

class Program
{
    static void Main(string[] args)
    {
        var modName = ParseModArg(args);

        if (string.IsNullOrEmpty(modName))
        {
            Console.WriteLine("Usage: --mod <ModName>");
            Console.WriteLine("Available mods: Bound");

            return;
        }

        var registry = new Dictionary<string, Func<IMod>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Bound"] = () => new BoundMod()
        };

        if (!registry.TryGetValue(modName, out var factory))
        {
            Console.WriteLine($"Mod not found: {modName}");
            Console.WriteLine("Available mods: " + string.Join(", ", registry.Keys));
            return;
        }

        var mod = factory();
        mod.Start();
    }

    static string? ParseModArg(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--mod" || args[i] == "-m")
            {
                if (i + 1 < args.Length) return args[i + 1];
                return null;
            }
            
            if (args[i].StartsWith("--mod="))
                return args[i].Substring("--mod=".Length);
        }
        return null;
    }
}