using Core.Logging.OpenSearch.Abstraction;
using Core.Logging.OpenSearch.Model;
using Microsoft.Extensions.Logging;

namespace Core.Logging.OpenSearch.Default_Implementations
{
    public class OverridenDefaultLogger : ILogger
    {

        private readonly ILogging _openSearchLogger;

        public OverridenDefaultLogger(ILogging openSearchLogger)
        {
            _openSearchLogger = openSearchLogger;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;




        /// <summary>
        /// TODO: implement using the configuration
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var log = new LogItem
            {

                EventId = eventId.Name,
                Message = state.ToString(),
                LogLevel = logLevel.ToString(),


            };

            if (exception != null)
            {
                log.StackTrace = exception.StackTrace;
                log.Message = exception.Message;
                log.ExceptionType = exception.GetType().Name;
            }



            _openSearchLogger.Log(log);
        }
    }
}
