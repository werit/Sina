using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ConsumeScheduleCreatedEvent> logger;


        public ConsumeScheduleCreatedEvent(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            logger  = serviceProvider.GetRequiredService<ILogger<ConsumeScheduleCreatedEvent>>();
        }

        public async Task SendEventMessage(string eventMessage)
        {
            using var scope = serviceProvider.CreateScope();
            // TODO: Toto deserializovanie crashne, ked tam psolem inu message...musim asi pouzit kluc,
            // aby som mal dobru message a lahko sa mi deserializovala
            logger.LogInformation($"Start of consuming event message: {eventMessage}");
            var itemCreated = JsonConvert.DeserializeObject<KafkaMessageScheduleCreated>(eventMessage);
            var recipeDbContext =
                scope.ServiceProvider.GetRequiredService<IRecipeDbContext<RecipeItem>>();
            await recipeDbContext.AddScheduleAsync(itemCreated.MessageValue, CancellationToken.None);
            logger.LogInformation($"End of consuming event message: {eventMessage}");

        }
    }
}