namespace EventAggregator
{
    public interface IEventAggregator
    {
        T GetEvent<T>() where T : EventBase, new();
    }
}