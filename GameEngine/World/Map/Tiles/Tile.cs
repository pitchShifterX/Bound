using MoonSharp.Interpreter;

namespace GameEngine.World.Map.Tiles
{
    public class Tile
    {
        public string? TextureId { get; set; }

        [MoonSharpHidden]
        public IntPtr? TextureHandle { get; set; }
    }
}