using GameEngine.Event.Input;
using GameEngine.Render;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.SharedInterface;
using GameEngine.World.Map;
using GameEngine.World.Map.Tiles;

namespace GameEngine
{
    public abstract class GameplayManager : ILoadable, IUpdatable, IRenderable
    {
        protected ICameraController? CameraController { get; private set; }
        protected ICameraView? CameraView { get; private set; }

        protected ISceneContext SceneContext { get; init; }
        protected IMapContext MapContext { get; init; }

        protected ITileRenderer? TileRenderer { get; set; }

        public GameplayManager(ISceneContext context)
        {
            SceneContext = context;

            MapContext = CreateMapContext();
        }

        public virtual void Load()
        {
            var camera = CreateCamera();

            CameraController = camera;
            CameraView = camera;

            TileRenderer = new TileRenderer(
                SceneContext,
                MapContext.TileCoordinateConverter!,
                CameraView
            );

            MapTileTextures();
        }

        public virtual void Unload()
        {
        }

        public virtual void ProcessInput(IRecordInput input)
        {
            if(input.IsKeyPressed(KeyCode.Up) || input.IsKeyPressed(KeyCode.W))
                CameraController?.MoveDirection(Direction.Up);
            
            if(input.IsKeyPressed(KeyCode.Down) || input.IsKeyPressed(KeyCode.S))
                CameraController?.MoveDirection(Direction.Down);
            
            if(input.IsKeyPressed(KeyCode.Left) || input.IsKeyPressed(KeyCode.A))
                CameraController?.MoveDirection(Direction.Left);
            
            if(input.IsKeyPressed(KeyCode.Right) || input.IsKeyPressed(KeyCode.D))
                CameraController?.MoveDirection(Direction.Right);
        }

        public virtual void Update(float? delta)
        {
            CameraController?.Update(delta);
        }

        public virtual void Render()
        {
            if(MapContext.Data == null) return;
            
            TileRenderer?.Render(MapContext.Data.Tiles!);
        }
        
        protected virtual IMapContext CreateMapContext()
        {
            var mapsDirectory = SceneContext.Paths.Maps;

            return new MapContext(mapsDirectory);
        }

        protected virtual Camera CreateCamera()
        {
            var mapSize = MapContext?.Data?.Metadata?.GetSize();

            if(mapSize == null)
                throw new System.Exception("Map size null, cannot create camera.");

            return new Camera(
                SceneContext.Settings.WindowSize,
                mapSize.Value
            );
        }

        protected virtual void MapTileTextures()
        {
            foreach(var row in MapContext?.Data?.Tiles!)
            {
                foreach(var tile in row)
                {
                    if(tile == null || tile.TextureId == null) continue;

                    var texture = SceneContext.GetById<Texture>(tile.TextureId);

                    if(texture == null) continue;

                    tile.TextureHandle = texture.Handle;
                }
            }
        }
    }
}