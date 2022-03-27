using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Confluent.Kafka;

namespace sina.messaging.contracts.MessageBroker.Kafka
{
    public interface IMessageProducer
    {
        Task ProduceMessageAsync(string topic,string message, CancellationToken cancellationToken);
    }
    

    public class KafkaRecipeItemCreatedProducer:IDisposable,IMessageProducer
    {
        private readonly IProducer<Null, string> producer;  
        public KafkaRecipeItemCreatedProducer()
        {
            
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:29092",
                ClientId = Dns.GetHostName(),
                
            };
            producer = new ProducerBuilder<Null, string>(config).Build();

        }

        public void Dispose()
        {
            producer.Flush();
            producer.Dispose();
        }

        public async Task ProduceMessageAsync(string topic, string message, CancellationToken cancellationToken)
        {
            await producer.ProduceAsync(topic, new Message<Null, string> {Value = message},cancellationToken);
        }
    }
}