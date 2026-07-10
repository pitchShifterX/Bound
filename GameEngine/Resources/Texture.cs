namespace GameEngine.Resources
{
    public class Texture : Resource
    {
        public Texture(string id, string path)
            : base(id, path) {}

        public override void Load()
        {
            throw new NotImplementedException();
        }

        protected override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}