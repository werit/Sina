using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using recipies_ms.Db;
using recipies_ms.Db.Models;
using sina.messaging.contracts.MessageBroker.ConsumerEvent;
using sina.messaging.contracts.MessageBroker.Kafka;

namespace recipies_ms.MicroserviceConsumer.ConsumeEvents
{
    public class ConsumeScheduleCreatedEvent: IConsumerEvent
    {
        private readonly IServiceProvider serviceProvider;

        public ConsumeScheduleCreatedEvent(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SendEventMessage(string eventMessage)
        {
            using var scope = serviceProvider.CreateScope();
            // TODO: Toto deserializovanie crashne, ked tam psolem inu message...musim asi pouzit kluc,
            // aby som mal dobru message a lahko sa mi deserializovala
            var itemCreated = JsonConvert.DeserializeObject<KafkaMessageScheduleCreated>(eventMessage);
            var recipeDbContext =
                scope.ServiceProvider.GetRequiredService<IRecipeDbContext<RecipeItem>>();
            await recipeDbContext.AddScheduleAsync(itemCreated.MessageValue, CancellationToken.None);
        }
    }
}