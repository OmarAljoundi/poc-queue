using DotNetCore.CAP.Filter;
using DotNetCore.CAP;
using Newtonsoft.Json;
using Core.Queuing.Model;
using Core.Queuing.Constants;
using Core.Logging.OpenSearch.Abstraction;
using Core.Logging.OpenSearch.Model;
using Microsoft.Extensions.Logging;

namespace Core.Queuing
{
    internal class QueueFilter : SubscribeFilter
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ILogging _logger;
        public QueueFilter(ICapPublisher capPublisher, ILogging logger)
        {
            _capPublisher = capPublisher;
            _logger = logger;

        }

        private Tuple<SequentialEvents, bool> TryGetIndex(List<SequentialEvents> events, int index)
        {
            try
            {
                var item = events[index];

                return Tuple.Create(item, true);
            }
            catch (Exception ex)
            {
                return Tuple.Create(new SequentialEvents(), true);
            }
        }
        public override async Task OnSubscribeExceptionAsync(ExceptionContext context)
        {

            var _event = string.Empty;
            if (context.DeliverMessage.Headers.TryGetValue(CustomHeader.EventHandlers, out _event))
            {
                var EventHandler = JsonConvert.DeserializeObject<Trigger>(_event);
                var CurrentEvent = int.Parse(context.DeliverMessage.Headers[CustomHeader.CurrentEvent]);



                if (EventHandler.SequentialEvents?.Count > 0)
                {
                    while (CurrentEvent != 0)
                    {
                        var result = TryGetIndex(EventHandler.SequentialEvents, CurrentEvent);
                        if (result.Item2)
                        {
                            var nextEvent = EventHandler.SequentialEvents[CurrentEvent];

                            Dictionary<string, string> headers = new()
                            {
                                { CustomHeader.CurrentStatus, EventStatus.RollingBack },
                                { CustomHeader.EventHandlers, _event },
                                { CustomHeader.CurrentEvent , (CurrentEvent - 1).ToString() },
                            };

                            await _capPublisher
                                .PublishDelayAsync(TimeSpan.FromSeconds(5),
                                nextEvent.TriggerName, context.DeliverMessage.Value, headers);
                        }
                        else
                        {
                            CurrentEvent--;
                        }
                    }


                }
            }
        }

        public override async Task OnSubscribeExecutedAsync(ExecutedContext context)
        {
            await _logger.Log(new LogItem
            {
                LogLevel = LogLevel.Information.ToString(),
                Message = JsonConvert.SerializeObject(context.DeliverMessage.Value),
                EventId = "EventId",
                Id = "Id",
                SentDate = DateTime.UtcNow.AddDays(-3),
            });

            var _event = string.Empty;
            if (context.DeliverMessage.Headers.TryGetValue(CustomHeader.EventHandlers, out _event))
            {
                var EventHandler = JsonConvert.DeserializeObject<Trigger>(_event);
                var CurrentEvent = int.Parse(context.DeliverMessage.Headers[CustomHeader.CurrentEvent]);
                var CurrentStatus = context.DeliverMessage.Headers[CustomHeader.CurrentStatus];




                if (EventHandler.SequentialEvents?.Count > 0)
                {
                    if (EventHandler.SequentialEvents.Count != CurrentEvent - 1)
                    {
                        var nextEvent = EventHandler.SequentialEvents[CurrentEvent - 1];

                        Dictionary<string, string> headers = new()
                            {
                                { CustomHeader.EventHandlers, _event },
                            };

                        if (CurrentStatus == EventStatus.Processing)
                        {
                            headers.Add(CustomHeader.CurrentEvent, (CurrentEvent + 1).ToString());
                            headers.Add(CustomHeader.CurrentStatus, EventStatus.Processing);
                        }
                        else
                        {
                            headers.Add(CustomHeader.CurrentEvent, (CurrentEvent - 1).ToString());
                            headers.Add(CustomHeader.CurrentStatus, EventStatus.RollingBack);
                        }

                        await _logger.Log(new LogItem
                        {
                            LogLevel = LogLevel.Information.ToString(),
                            Message = JsonConvert.SerializeObject(context.DeliverMessage.Value),
                            EventId = $"Publishing Next Event in 5 seconds {nextEvent.TriggerName}",
                            Id = "Id",
                            SentDate = DateTime.UtcNow.AddDays(3),
                        });

                        await _capPublisher
                            .PublishDelayAsync(TimeSpan.FromSeconds(5),
                            nextEvent.TriggerName, context.DeliverMessage.Value, headers);

                    }
                }
            }
        }
    }
}
