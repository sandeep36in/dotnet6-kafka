namespace Core
{
    public interface IPublisher<T>:IDisposable where T : class
    {
        Task<bool> PublishAsync(T message, CancellationToken cancellationToken);
    }
}
