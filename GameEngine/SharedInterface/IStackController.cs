namespace GameEngine.SharedInterface
{
    public interface IStackController<T>
    {
        public T? Current { get; }
        public void Push(Func<T> item);
        public void Pop();
        public void Replace(Func<T> item);
    }
}