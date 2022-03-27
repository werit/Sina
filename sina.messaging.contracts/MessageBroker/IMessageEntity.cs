namespace sina.messaging.contracts.MessageBroker
{
    public interface IMessageEntity<T> where T: class
    {
        T MessageValue { get; set; }
    }
}