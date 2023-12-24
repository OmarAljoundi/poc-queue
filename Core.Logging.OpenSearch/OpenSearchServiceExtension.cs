using Core.Logging.OpenSearch.Abstraction;
using Core.Logging.OpenSearch.Default_Implementations;
using Core.Logging.OpenSearch.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Logging.OpenSearch
{
    public static class OpenSearchServiceExtension
    {
        public static IServiceCollection AddOpenSearchLogging(this IServiceCollection services,OpenSearchConfig openSearchConfig) 
        {
            services.AddSingleton<ILogging, Default_Implementations.Logging>
                (x => new Default_Implementations.Logging(openSearchConfig));
            services.AddSingleton<ILogger, OverridenDefaultLogger>();
            services.AddSingleton<ILogging, Default_Implementations.Logging>
                (x => new Default_Implementations.Logging(openSearchConfig));

            return services;
        }
    }
}
