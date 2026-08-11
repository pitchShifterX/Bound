using System.Reflection;
using SDL2;

namespace GameEngine.Graphics.Primitives
{
    public readonly struct Color
    {
        public readonly byte R;
        public readonly byte G;
        public readonly byte B;
        public readonly byte A;

        public Color(byte r, byte g, byte b, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public SDL.SDL_Color ToSDL()
        {
            return new SDL.SDL_Color
            {
                r = R,
                g = G,
                b = B,
                a = A
            };
        }

        public static Color FromSDL(SDL.SDL_Color color)
        {
            return new Color(color.r, color.g, color.b, color.a);
        }

        public static Color FromHex(uint hex)
        {
            return new Color(
                (byte)((hex >> 16) & 0xFF),
                (byte)((hex >> 8) & 0xFF),
                (byte)(hex & 0xFF)
            );
        }

        public static Color FromString(string color)
        {
            if(color == null) return White;
            
            var field = typeof(Color).GetField(
                name: color,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase
            );

            if (field?.GetValue(null) is Color value)
                return value;

            return White;
        }

        public static implicit operator SDL.SDL_Color(Color color)
            => color.ToSDL();

        public static implicit operator Color(SDL.SDL_Color color)
            => FromSDL(color);

        public static readonly Color White  = new(255, 255, 255);
        public static readonly Color Black  = new(0, 0, 0);
        public static readonly Color Red    = new(255, 0, 0);
        public static readonly Color Green  = new(0, 255, 0);
        public static readonly Color Blue   = new(0, 0, 255);
        public static readonly Color Transparent = new (0, 0, 0, 0);
    }
}