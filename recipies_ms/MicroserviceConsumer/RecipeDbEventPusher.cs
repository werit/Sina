using System;
using recipies_ms.MicroserviceConsumer.ConsumeEvents;
using sina.messaging.contracts.MessageBroker.Kafka;

namespace recipies_ms.MicroserviceConsumer
{
    public class RecipeDbEventPusher
    {
        public RecipeDbEventPusher(IKafkaConsumerRepository kafkaConsumerRepository, IServiceProvider serviceProvider)
        {
            var createdConsumer = kafkaConsumerRepository.GetRecipeCreatedConsumer();
            createdConsumer.AddConsumer(new ConsumeScheduleCreatedEvent(serviceProvider));
        }
    }
}