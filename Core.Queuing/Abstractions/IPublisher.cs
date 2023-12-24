using Core.Queuing.Model;
using DotNetCore.CAP;
namespace Core.Queuing.Abstractions
{
    public interface IPublisher
    {
        Task PublishAsync<T>(T? contentObj, List<SequentialEvents> sequentialEvents, CancellationToken cancellationToken = default);
        Task PublishAsync<T>(T? contentObj, string UniqueId, CancellationToken cancellationToken = default);
    }
}
