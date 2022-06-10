using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sina.messaging.contracts.MessageBroker.Kafka;

namespace sina.messaging.contracts.MessageBroker.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddSinaKafkaProducer(this IServiceCollection services)
        {
            services.AddSingleton<IMessageProducer, KafkaRecipeItemCreatedProducer>();
            return services;
        }

        public static IServiceCollection AddSinaKafkaConsumers(this IServiceCollection services, string  groupId,string topic)
        {
            var  kafkaRecipeItemCreatedCreatedConsumer= new KafkaRecipeItemCreatedCreatedConsumer(groupId,topic);
            services.AddSingleton<IHostedService>(kafkaRecipeItemCreatedCreatedConsumer);
            services.AddSingleton<IMessageCreatedConsumer>(kafkaRecipeItemCreatedCreatedConsumer);
            services.AddSingleton<IKafkaConsumerRepository, KafkaConsumerRepository>();
            return services;
        }
    }
}