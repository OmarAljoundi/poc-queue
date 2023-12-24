using Core.Logging.OpenSearch.Abstraction;
using Core.Queuing.Abstractions;
using Core.Queuing.Constants;
using Core.Queuing.Model;
using DotNetCore.CAP;
using Newtonsoft.Json;


namespace CAP.RabbitMQ.Core.Default_Implementations
{
    public sealed class Publisher : IPublisher
    {
        private readonly ICapPublisher _publisher;
        private readonly IEventConfiguration _eventConfiguration;
        private readonly ILogging _logging;

        public Publisher(ICapPublisher publisher, IEventConfiguration eventConfiguration, ILogging logging)
        {
            _publisher = publisher;
            _eventConfiguration = eventConfiguration;
            _logging = logging;
        }

        public async Task PublishAsync<T>(T? contentObj, List<SequentialEvents> sequentialEvents,
            CancellationToken cancellationToken = default)
        {
/*

            Dictionary<string, string> headers = new()

           

            {
                CustomHeader.EventHandlers,
                    JsonConvert.SerializeObject(events, Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    })
                },
                {
                CustomHeader.CurrentEvent,
                    "1"
                },
                {
                CustomHeader.CurrentStatus,
                    EventStatus.Processing
                }
            await _publisher.PublishAsync(events.UniqueId, contentObj, headers, cancellationToken);*/
        }


        public async Task PublishAsync<T>(T? contentObj, string UniqueId,
          CancellationToken cancellationToken = default)
        {
            /*Trigger trigger =
                (_eventConfiguration.Events.Triggers?
                .Find(x => x.UniqueId.Equals(UniqueId))) ?? throw new Exception("The Provided Unique ID has no match");


            await PublishAsync(contentObj, trigger.SequentialEvents, cancellationToken);*/

        }
    }

}
