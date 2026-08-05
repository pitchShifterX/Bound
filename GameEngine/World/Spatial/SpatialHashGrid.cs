using GameEngine.Utilities;

namespace GameEngine.World.Spatial
{
    /// <summary>
    /// The purpose of this class is to divide the map into sections. 
    /// This makes it efficient to query nearby entities rather than 
    /// all entities on the map. This class offers tools for querying, 
    /// inserting, etc. entities.
    /// </summary>
    public class SpatialHashGrid
    {
        /// <summary>
        /// A lookup for entities within a grid cell.
        /// </summary>
        private Dictionary<GridCell, HashSet<int>> _cells = [];

        public void Insert(int entity, Rectangle<float> bounds)
        {
            foreach(var cell in getCells(bounds))
            {
                if (!_cells.TryGetValue(cell, out var entities))
                {
                    entities = [];
                    _cells[cell] = entities;
                }

                entities.Add(entity);
            }
        }

        public void Remove(int entity, Rectangle<float> bounds)
        {
            foreach(var cell in getCells(bounds))
            {
                if (_cells.TryGetValue(cell, out var entities))
                {
                    entities.Remove(entity);

                    if (entities.Count == 0)
                        _cells.Remove(cell);
                }
            }
        }

        public void Update(
            int entity,
            Rectangle<float> oldBounds,
            Rectangle<float> newBounds
        )
        {
            Remove(entity, oldBounds);
            Insert(entity, newBounds);
        }

        public IEnumerable<int> Query(Rectangle<float> bounds)
        {
            var results = new HashSet<int>();

            foreach(var cell in getCells(bounds))
            {
                if (!_cells.TryGetValue(cell, out var entities))
                    continue;

                foreach(var entity in entities)
                {
                    results.Add(entity);
                }
            }
            
            return results;
        }

        private IEnumerable<GridCell> getCells(Rectangle<float> bounds)
        {
            int minX = (int)(bounds.X / Constants.CellSize);
            int maxX = (int)((bounds.X + bounds.Width) / Constants.CellSize);

            int minY = (int)(bounds.Y / Constants.CellSize);
            int maxY = (int)((bounds.Y + bounds.Height) / Constants.CellSize);

            for(int x = minX; x <= maxX; x++)
            {
                for(int y = minY; y <= maxY; y++)
                {
                    yield return new GridCell(x, y);
                }
            }
        }
    }
}