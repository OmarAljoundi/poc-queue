using Core.Logging.OpenSearch.Abstraction;
using OpenSearch.Client;


namespace Core.Logging.OpenSearch.Default_Implementations
{
    public class OpenSearchQuery : IOpenSearchQuery
    {
        private readonly IOpenSearchClient _client;
        private const string INDEX_NAME = "eventware";
        public OpenSearchQuery(IOpenSearchClient client)
        {
            _client = client;
        }
    }

    
}
