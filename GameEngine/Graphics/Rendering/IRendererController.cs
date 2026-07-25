using GameEngine.Graphics.Drawing;
using GameEngine.Resources;
using SDL2;

namespace GameEngine.Graphics.Rendering
{
    public interface IRendererController : IDrawPrimitive
    {
        public IntPtr Renderer { get; }
        public void Create();

        /// <summary>
        /// <para>Draw text that is intended to be updated per frame.</para>
        /// <para>For example, you would use this if you're rendering framerate 
        /// or score.</para>
        /// <para>It is recommended to use the UI tooling over this.</para>
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="destination"></param>
        public void DrawDynamicText(Font font, string text, SDL.SDL_Color color, SDL.SDL_Rect destination);
        public void Present();
        public void Clear();
        public void Destroy();
    }
}