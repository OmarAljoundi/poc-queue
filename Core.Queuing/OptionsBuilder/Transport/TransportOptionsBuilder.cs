namespace Core.Queuing.OptionsBuilder.Transport
{
    public class TransportOptionsBuilder
    {
        public TransportProviders Provider { get; set; }
        public string ConnectionString { get; set; }
        public string DefaultGroupName { get; set; }
        public int FailedRetryCount { get; set; }

    }
}

