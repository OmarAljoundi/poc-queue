using CAP.RabbitMQ.Core.Default_Implementations;
using Core.Logging.OpenSearch;
using Core.Logging.OpenSearch.Model;
using Core.Queuing.Abstractions;
using Core.Queuing.OptionsBuilder;
using Core.Queuing.OptionsBuilder.Database;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Queuing
{
    public class QueueOptionsBuilder 
    {
        public IServiceCollection Services { get; }
        internal DatabaseOptionsBuilder MySQLOptionsBuilder { get; set; }
        internal DatabaseOptionsBuilder PostgresOptionsBuilder { get; set; }
        internal DatabaseOptionsBuilder SQLServerOptionsBuilder { get; set; }
        internal DatabaseOptionsBuilder MongoDBOptionsBuilder { get; set; }
        internal RabbitMQOptions RabbitMQOptionsBuilder { get; set; }
        internal KafkaOptions KafkaOptionsBuilder { get; set; }

        public DefaultOptions DefaultOptions { get;set; }
        public QueueOptionsBuilder(IServiceCollection services,
            DatabaseOptionsBuilder MySQLOptionsBuilder, 
            DatabaseOptionsBuilder PostgresOptionsBuilder,
            DatabaseOptionsBuilder SQLServerOptionsBuilder, 
            DatabaseOptionsBuilder MongoDBOptionsBuilder,
            RabbitMQOptions RabbitMQOptionsBuilder,
            KafkaOptions KafkaOptionsBuilder,
            DefaultOptions DefaultOptions
            )
        {
            Services = services;
            this.MySQLOptionsBuilder = MySQLOptionsBuilder;
            this.PostgresOptionsBuilder = PostgresOptionsBuilder;
            this.SQLServerOptionsBuilder = SQLServerOptionsBuilder;
            this.MongoDBOptionsBuilder = MongoDBOptionsBuilder;
            this.RabbitMQOptionsBuilder = RabbitMQOptionsBuilder;
            this.KafkaOptionsBuilder = KafkaOptionsBuilder;
            this.DefaultOptions = DefaultOptions;
        }

        public QueueOptionsBuilder AddRabbitMQ(Action<RabbitMQOptions> configure)
        {
            configure(RabbitMQOptionsBuilder);
            return this;
        }

        public QueueOptionsBuilder AddKafka(Action<KafkaOptions> configure)
        {
            configure(KafkaOptionsBuilder);
            return this;
        }


        public QueueOptionsBuilder AddMySQL(Action<DatabaseOptionsBuilder> configure)
        {
            configure(MySQLOptionsBuilder);
            return this;
        }

        public QueueOptionsBuilder AddSqlServer(Action<DatabaseOptionsBuilder> configure)
        {
            configure(SQLServerOptionsBuilder);
            return this;
        }

        public QueueOptionsBuilder AddPostgres(Action<DatabaseOptionsBuilder> configure)
        {
            configure(PostgresOptionsBuilder);
            return this;
        }

        public QueueOptionsBuilder AddMongoDB(Action<DatabaseOptionsBuilder> configure)
        {
            configure(MongoDBOptionsBuilder);
            return this;
        }


        public QueueOptionsBuilder AddTransactionFile(string? prefix = "transaction")
        {
            Services.AddSingleton<IEventConfiguration, EventConfiguration>(x => new EventConfiguration(prefix));
            return this;
        }

        public QueueOptionsBuilder AddOpenSearch(Action<OpenSearchConfig> configure)
        {
            var openSearchConfig = new OpenSearchConfig();
            configure(openSearchConfig);
       
            if(!string.IsNullOrEmpty(openSearchConfig.UserName) &&
                !string.IsNullOrEmpty(openSearchConfig.Password) &&
                !string.IsNullOrEmpty(openSearchConfig.OpenSearchHostUrl))
            {
                Services.AddOpenSearchLogging(openSearchConfig);
            }
            return this;
        }
    }
}

