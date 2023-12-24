using Core.Logging.OpenSearch.Model;

namespace Core.Logging.OpenSearch.Abstraction
{

    public interface ILogging
    {
        public Task Log(LogItem item);

        public Task Log(IList<LogItem> items);
    }
}
