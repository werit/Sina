using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using sina.messaging.contracts.MessageBroker.ConsumerEvent;
using sina.messaging.contracts.MessageBroker.Kafka;
using sina.planning.Db;
using sina.planning.Db.Models;

namespace sina.planning.MicroserviceConsumer.ConsumeEvents
{
    public class ConsumeCreatedEvent : IConsumerEvent
    {
        private readonly IServiceProvider serviceProvider;

        public ConsumeCreatedEvent(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SendEventMessage(string eventMessage)
        {
            using var scope = serviceProvider.CreateScope();
            // TODO: Toto deserializovanie crashne, ked tam psolem inu message...musim asi pouzit kluc,
            // aby som mal dobru message a lahko sa mi deserializovala
            var itemCreated = JsonConvert.DeserializeObject<KafkaMessageItemCreated>(eventMessage);
            var schedulingDbContext =
                scope.ServiceProvider.GetRequiredService<IRecipeSchedulingDbContext<RecipeScheduleItem>>();
            await schedulingDbContext.AddRecipeAsync(itemCreated.MessageValue, CancellationToken.None);
        }
    }
}