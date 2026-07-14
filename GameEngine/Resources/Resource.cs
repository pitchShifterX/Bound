namespace GameEngine.Resources
{
    public abstract class Resource : IDisposable
    {
        public string Id { get; init; }
        public string Path { get; init; }
        public IntPtr Handle { get; protected set; }
        protected bool Disposed { get; set; } = false;

        public Resource(string id, string path)
        {
            Id = id;
            Path = path;
        }

        /// <summary>
        /// Load in the resource e.g. IMG_LoadTexture
        /// </summary>
        public virtual void Load(){}

        /// <summary>
        /// Unload the resource. This method is called 
        /// during disposal. If you have any hanging 
        /// IntPtrs related to the resource, set them 
        /// to zero here.
        /// </summary>
        protected abstract void Destroy();

        /// <summary>
        /// Call this when ready to unload the resource.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(Disposed)
                return;
            
            Destroy();

            Disposed = true;
        }

        ~Resource()
        {
            Dispose(false);
        }
    }
}