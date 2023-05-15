using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using sina.messaging.contracts.MessageBroker.ConsumerEvent;

namespace sina.messaging.contracts.MessageBroker.Kafka
{
    public interface IMessageCreatedConsumer :IDisposable
    {
        void StartMessageConsumption();
        void AddConsumer(IConsumerEvent consumerEvent);
    }
    public class KafkaRecipeItemCreatedCreatedConsumer:IMessageCreatedConsumer,IHostedService
    {
        private readonly IConsumer<Null, string> consumerBuilder;
        private List<IConsumerEvent> eventsListeners = new();
        private readonly string topic;

        public KafkaRecipeItemCreatedCreatedConsumer(string groupId,string topic)
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = "localhost:29092",
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true
            };
            consumerBuilder = new ConsumerBuilder<Null, string>(config).Build();
            this.topic = topic;

        }
        public void Dispose()
        {
            consumerBuilder.Dispose();
        }

        public void StartMessageConsumption()
        {
            throw new NotImplementedException();
        }

        public void AddConsumer(IConsumerEvent consumerEvent)
        {
            eventsListeners.Add(consumerEvent);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                consumerBuilder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                try {
                    while (true) {
                        try
                        {
                            var consumeResult = consumerBuilder.Consume 
                                (cancelToken.Token);
                            Debug.WriteLine($"Message from kafka is: {consumeResult.Message.Value}");
                            foreach (var eventsListener in eventsListeners)
                            {
                                eventsListener.SendEventMessage(consumeResult.Message.Value);
                            }
                            // var orderRequest = JsonSerializer.Deserialize 
                            //     <OrderProcessingRequest> 
                            //     (consumer.Message.Value);
                            // Debug.WriteLine($"Processing Order Id: 
                            // {orderRequest.OrderId}");
                        }
                        catch (ConsumeException e)
                        {
                            Debug.WriteLine($"Exception during trying to consume topic: {topic}, Exception: {e}");
                        }

                    }
                } catch (OperationCanceledException) {
                    consumerBuilder.Close();
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}