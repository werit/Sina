using Newtonsoft.Json;

namespace sina.messaging.contracts.MessageBroker.Kafka
{
    public class KafkaMessageItemCreated : IMessageEntity<RecipeItemCreated>
    {
        public string MessageKey { get; set; }
        public RecipeItemCreated MessageValue { get; set; }

        public override string ToString()
        {
            return
                $"Message key is: '{MessageKey}' and recipe name is: {MessageValue.Name} and id is: {MessageValue.RecipeId}";
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}