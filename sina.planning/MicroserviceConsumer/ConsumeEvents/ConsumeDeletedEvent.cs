using System.Threading.Tasks;
using sina.messaging.contracts.MessageBroker.ConsumerEvent;

namespace sina.planning.MicroserviceConsumer.ConsumeEvents
{
    public class ConsumeDeletedEvent:IConsumerEvent
    {
        public Task SendEventMessage(string eventMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}