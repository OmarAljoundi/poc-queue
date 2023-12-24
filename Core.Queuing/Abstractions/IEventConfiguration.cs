using Core.Queuing.Model;

namespace Core.Queuing.Abstractions
{
    public interface IEventConfiguration
    {
        public MainTriggers Events { get; init; }
        public MainTriggers LoadTriggers(string prefix);
    }
}
