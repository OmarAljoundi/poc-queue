using Core.Logging.OpenSearch.Abstraction;
using Core.Logging.OpenSearch.Model;
using OpenSearch.Client;


namespace Core.Logging.OpenSearch.Default_Implementations
{
    public class Logging : ILogging
    {
        private readonly OpenSearchConfig _config;
        private readonly IOpenSearchClient _client;
        private const string INDEX_NAME = "eventware";


        public Logging(OpenSearchConfig config)
        {
            _config = config;

            if (string.IsNullOrEmpty(_config?.UserName) || string.IsNullOrEmpty(_config?.Password))
                throw new Exception($"OpenSearch logging plugin configuration cannot be empty: url, username, or password are empty");

            _client = CreateClient();
        }


        
        public async Task Log(LogItem log)
        {
            log.Id = Guid.NewGuid().ToString();
            var response = await _client.IndexAsync(log,
                x => x.Index(INDEX_NAME));
        }

        public async Task Log(IList<LogItem> logs)
        {
            var response = await _client.IndexManyAsync(logs, INDEX_NAME);
        }

        private IOpenSearchClient CreateClient()
        {
            var address = !string.IsNullOrEmpty(_config?.OpenSearchHostUrl) ? new Uri(_config?.OpenSearchHostUrl) : null;

            var config = new ConnectionSettings(new Uri("http://localhost:9200", UriKind.Absolute))
                .EnableDebugMode()
                .DefaultIndex(INDEX_NAME)
                .PrettyJson()
                .BasicAuthentication(_config.UserName, _config.Password)
                .DisableDirectStreaming();

            var client = address == null ? new OpenSearchClient() :
                new OpenSearchClient(config);

            return client;
        }

    }
}
