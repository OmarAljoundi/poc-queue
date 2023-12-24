using Core.Queuing.OptionsBuilder;
using Core.Queuing.OptionsBuilder.Database;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;


namespace Core.Queuing
{

    public static class QueuingServiceExtensions
    {
        public static IServiceCollection AddQueue(this IServiceCollection services, Action<QueueOptionsBuilder> configure)
        {
            var MySQLOptionsBuilder = new DatabaseOptionsBuilder();
            var PostgresOptionsBuilder = new DatabaseOptionsBuilder();
            var SQLServerOptionsBuilder = new DatabaseOptionsBuilder();
            var MongoDBOptionsBuilder = new DatabaseOptionsBuilder();
            var DefaultOptions = new DefaultOptions();

            var RabbitMQOptionsBuilder = new RabbitMQOptions();
            var KafkaOptionsBuilder = new KafkaOptions();

            var builder =
                new QueueOptionsBuilder(services,
                MySQLOptionsBuilder,
                PostgresOptionsBuilder,
                SQLServerOptionsBuilder,
                MongoDBOptionsBuilder,
                RabbitMQOptionsBuilder,
                KafkaOptionsBuilder,
                DefaultOptions);

            configure(builder);


            services.AddCap(options =>
            {

                if(MySQLOptionsBuilder is not null)
                {
                    options.UseMySql(MySQLOptionsBuilder.ConnectionString);
                }
                else if(PostgresOptionsBuilder is not null)
                {
                    options.UsePostgreSql(PostgresOptionsBuilder.ConnectionString);
                }
                else if(SQLServerOptionsBuilder is not null)
                {
                    options.UseSqlServer(SQLServerOptionsBuilder.ConnectionString);
                }
                else if(MongoDBOptionsBuilder is not null)
                {
                    options.UseMongoDB(MongoDBOptionsBuilder.ConnectionString);
                }
                else
                {
                    throw new Exception("Missing Provider"); //TODO: Throw Custom Exception
                }

                if (RabbitMQOptionsBuilder is not null)
                {
                    options.UseRabbitMQ(x =>
                    {
                        x.ConnectionFactoryOptions = RabbitMQOptionsBuilder.ConnectionFactoryOptions;
                    });
                }
                else if (KafkaOptionsBuilder is not null)
                {
                    options.UseKafka(KafkaOptionsBuilder.Servers); // TODO:Pass All the params
                }

               
                options.DefaultGroupName = DefaultOptions.DefaultGroupName;
                options.FailedRetryCount = DefaultOptions.FailedRetryCount;
                options.CollectorCleaningInterval = DefaultOptions.CollectorCleaningInterval;

                options.ConsumerThreadCount = DefaultOptions.ConsumerThreadCount;
                options.EnableConsumerPrefetch = DefaultOptions.EnableConsumerPrefetch;
                options.FailedMessageExpiredAfter = DefaultOptions.FailedMessageExpiredAfter;

                options.FailedRetryInterval = DefaultOptions.FailedRetryInterval;
                options.FailedThresholdCallback = DefaultOptions.FailedThresholdCallback;
                options.FallbackWindowLookbackSeconds = DefaultOptions.FallbackWindowLookbackSeconds;

                options.GroupNamePrefix = DefaultOptions.GroupNamePrefix;
                options.Version = DefaultOptions.Version;
                
                options.UseStorageLock = DefaultOptions.UseStorageLock;
                options.UseDispatchingPerGroup = DefaultOptions.UseDispatchingPerGroup;
                options.TopicNamePrefix = DefaultOptions.TopicNamePrefix;
                options.SucceedMessageExpiredAfter = DefaultOptions.SucceedMessageExpiredAfter;
           


                options.UseDashboard(path => path.PathMatch = "/cap-dashboard"); // TODO: Pass Option To enable the dashboard
            })
                .AddSubscribeFilter<QueueFilter>();



            return services;
        }


    }


}

