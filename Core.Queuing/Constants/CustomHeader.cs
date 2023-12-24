using DotNetCore.CAP.Messages;

namespace Core.Queuing.Constants
{
    public static class CustomHeader
    {
        public const string EventHandlers = "cap-event-handlers";
        public const string CurrentEvent = "cap-current-event";
        public const string CurrentStatus = "cap-current-status";
    }
}
