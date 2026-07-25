using GameEngine.Event.Input;
using GameEngine.Graphics.Cameras;
using GameEngine.Graphics.Primitives;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.SharedInterface;
using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Systems;
using GameEngine.World.Map;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Unit;

namespace GameEngine
{
    public abstract class GameplayManager : ILoadable, IUpdatable, IRenderable
    {
        /// <summary>
        /// Handles direct control of the Camera: move speed, zoom, etc.
        /// </summary>
        protected ICameraController? CameraController { get; private set; }

        /// <summary>
        /// Provides data for getting screen position or world coordinates.
        /// </summary>
        protected ICameraView? CameraView { get; private set; }

        /// <summary>
        /// Maintains resources and provides API for interacting with 
        /// core systems.
        /// </summary>
        protected ISceneContext SceneContext { get; init; }

        /// <summary>
        /// Loads and maintains map data.
        /// </summary>
        protected IMapContext MapContext { get; init; }

        /// <summary>
        /// Simple tile renderer; separated from ECS for improved 
        /// performance.
        /// </summary>
        protected ITileRenderer? TileRenderer { get; set; }

        /// <summary>
        /// Core service for managing entities and components. This is 
        /// often passed around to systems for manipulating components 
        /// on entities.
        /// </summary>
        protected ECSService ECSService { get; init; } = new();

        /// <summary>
        /// Mods register unit prefabs here. Entities reference prefabs 
        /// by name to get their pre-defined components.
        /// </summary>
        protected UnitPrefabRegistry UnitRegistry { get; init; } = new();
        protected MapAPI MapAPI { get; init; }

        protected MapInitializationSystem? MapInitializationSystem { get; set; }
        protected SelectionSystem SelectionSystem { get; init; } = new();
        protected RenderTextureSystem RenderTextureSystem { get; init; } = new();

        public GameplayManager(ISceneContext context)
        {
            SceneContext = context;

            RegisterModContent();

            MapAPI = new MapAPI(ECSService, UnitRegistry);

            MapContext = CreateMapContext();
        }

        public virtual void Load()
        {
            var camera = CreateCamera();

            CameraController = camera;
            CameraView = camera;

            if(MapContext.Data != null)
            {
                MapInitializationSystem = new MapInitializationSystem(ECSService, MapContext.Data);
                MapInitializationSystem.InitializeMapEntities();
            }

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
            if(input.IsKeyPressed(KeyCode.Up))
                CameraController?.MoveDirection(Direction.Up);
            
            if(input.IsKeyPressed(KeyCode.Down))
                CameraController?.MoveDirection(Direction.Down);
            
            if(input.IsKeyPressed(KeyCode.Left))
                CameraController?.MoveDirection(Direction.Left);
            
            if(input.IsKeyPressed(KeyCode.Right))
                CameraController?.MoveDirection(Direction.Right);

            if(input.MouseScrollY > 0)
                CameraController?.SetZoom(2.5f);
            
            if(input.MouseScrollY < 0)
                CameraController?.SetZoom(2);

            if(input.WasMouseButtonPressed(MouseButton.Left))
            {
                var worldPosition = CameraView?.ScreenPositionToWorldPosition(
                    input.MousePositionX, input.MouseScrollY
                );

                if(worldPosition == null)
                    return;

                SelectionSystem.HandleClick(
                    ECSService,
                    worldPosition.Value.X, worldPosition.Value.Y,
                    "one"
                );
            }
        }

        public virtual void Update(float? delta)
        {
            CameraController?.Update(delta);
        }

        public virtual void Render()
        {
            if(MapContext.Data == null || CameraView == null) return;
            
            TileRenderer?.Render(MapContext.Data.Tiles!);

            RenderTextureSystem.DrawTextures(ECSService, SceneContext, CameraView);
            RenderTextureSystem.DrawBorder(ECSService, SceneContext, CameraView);
        }
        
        protected virtual IMapContext CreateMapContext()
        {
            var mapsDirectory = SceneContext.Paths.Maps;

            return new MapContext(mapsDirectory, MapAPI);
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

        /// <summary>
        /// Register mod prefabs and other content that will be used by map files.
        /// </summary>
        protected virtual void RegisterModContent()
        {
            
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