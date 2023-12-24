using Core.Queuing.Abstractions;
using Core.Queuing.Constants;
using Core.Queuing.Exceptions;
using Core.Queuing.Model;
using Newtonsoft.Json;

namespace CAP.RabbitMQ.Core.Default_Implementations
{
    public class EventConfiguration : IEventConfiguration
    {
        public EventConfiguration(string prefix = "transaction") 
        {
            Events = LoadTriggers(prefix);
        }

        public MainTriggers Events { get; init; }

        public MainTriggers LoadTriggers(string prefix)
        {
            var EnvironmentVariable = Environment.GetEnvironmentVariable(Variables.Environment);
            var transactionFile = !string.IsNullOrEmpty(EnvironmentVariable) ? $"{prefix}.{EnvironmentVariable}.json" : $"{prefix}.json";
            var filePath = Path.Combine(AppContext.BaseDirectory, transactionFile);

            if (!File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);

                try
                {
                    return JsonConvert.DeserializeObject<MainTriggers>(jsonContent);
                }
                catch (Exception exception)
                {
                    throw new TransactionFileInCorrectFormatException($"{prefix}.{EnvironmentVariable}.json", exception);
                }
                
            }
            return new MainTriggers();

        }
    }
}
