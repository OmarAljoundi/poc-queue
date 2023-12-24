namespace Core.Logging.OpenSearch.Model
{
    public class LogItem
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string LogLevel { get; set; }
        public string EventId { get; set; }
        public string ExceptionType { get; set; }
        public DateTime SentDate { get; set; }
        public string StackTrace { get; set; }
    }
}
