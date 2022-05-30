using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace sina.messaging.contracts.MessageBroker.Kafka
{
    public interface IKafkaConsumerRepository
    {
        // Task ConsumeMessageAsync(string topic,string message, CancellationToken cancellationToken);
        IMessageCreatedConsumer GetRecipeCreatedConsumer();
    }
    
    public class KafkaConsumerRepository:IKafkaConsumerRepository,IDisposable 
    {
        private readonly IMessageCreatedConsumer messageCreatedConsumer;

        public KafkaConsumerRepository(IMessageCreatedConsumer messageCreatedConsumer)
        {
            
            this.messageCreatedConsumer = messageCreatedConsumer;
        }
        
        public IMessageCreatedConsumer GetRecipeCreatedConsumer()
        {
            return messageCreatedConsumer;
        }

        public void Dispose()
        {
            messageCreatedConsumer.Dispose();
        }
    }
}