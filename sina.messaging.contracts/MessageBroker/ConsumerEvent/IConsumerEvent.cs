using System.Threading.Tasks;

namespace sina.messaging.contracts.MessageBroker.ConsumerEvent
{
    public interface IConsumerEvent
    {
        Task SendEventMessage(string eventMessage);
    }
}