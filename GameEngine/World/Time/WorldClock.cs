namespace GameEngine.World.Time
{
    public class WorldClock : IClock
    {
        public float ElapsedSeconds { get; private set; }
        public float DeltaSeconds { get; private set; }

        public void Update(float? delta)
        {
            if(delta == null) return;

            DeltaSeconds = delta.Value;
            ElapsedSeconds += delta.Value;
        }
    }
}