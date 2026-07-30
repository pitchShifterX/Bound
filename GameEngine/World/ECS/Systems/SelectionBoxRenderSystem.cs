using GameEngine.Graphics.Primitives;
using GameEngine.Graphics.Rendering;
using GameEngine.World.Input;

namespace GameEngine.World.ECS.Systems
{
    public class SelectionBoxRenderSystem
    {
        public void Draw(SelectionService selection, IRenderContext renderContext)
        {
            if(!selection.IsDragging) return;

            var rect = selection.SelectionRectangle.To<float>();

            renderContext.DrawRectangle(rect, Color.Green);
        }
    }
}