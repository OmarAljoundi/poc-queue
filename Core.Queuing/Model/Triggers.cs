namespace Core.Queuing.Model
{

    public class MainTriggers
    {
        public List<Trigger> Triggers { get; set; }
    }
    public class Trigger
    {
        public string UniqueId { get; set; }
        public List<SequentialEvents> SequentialEvents { get; set; }

        public Trigger()
        {
            SequentialEvents = new();
        }
    }

    public class SequentialEvents
    {
        public string TriggerName { get; set; }
        public List<string> OnFailure { get; set; } = new();
        public bool RequeueIfFailureFailed { get; set; }
        public int? WaitInSecondsBeforePublishing { get; set; }
    }
}
