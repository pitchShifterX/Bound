namespace GameEngine.Resources
{
    public abstract class Resource : IDisposable
    {
        public string Id { get; init; }
        public string Path { get; init; }
        protected bool Disposed { get; set; } = false;

        public Resource(string id, string path)
        {
            Id = id;
            Path = path;
        }

        public abstract void Load();
        protected abstract void Destroy();

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