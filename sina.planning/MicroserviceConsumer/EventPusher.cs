using System;
using sina.messaging.contracts.MessageBroker.Kafka;
using sina.planning.MicroserviceConsumer.ConsumeEvents;

namespace sina.planning.MicroserviceConsumer
{
    public class EventPusher
    {

        public EventPusher(IKafkaConsumerRepository kafkaConsumerRepository,IServiceProvider serviceProvider)
        {
            var createdConsumer = kafkaConsumerRepository.GetRecipeCreatedConsumer();
            createdConsumer.AddConsumer(new ConsumeCreatedEvent(serviceProvider));
        }
    }
}