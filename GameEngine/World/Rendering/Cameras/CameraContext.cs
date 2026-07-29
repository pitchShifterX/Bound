namespace GameEngine.World.Rendering.Cameras
{
    public class CameraContext
    {
        /// <summary>
        /// Handles direct control of the Camera: move speed, zoom, etc.
        /// </summary>
        public ICameraController Controller { get; init; }

        /// <summary>
        /// Provides data for getting screen position or world coordinates.
        /// </summary>
        public ICameraView View { get; init; }

        public CameraContext(Camera camera)
        {
            Controller = camera;
            View = camera;
        }
    }
}