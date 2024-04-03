
namespace ApplyForJob.Services
{
    public interface ITokenReceiverHandler<TEvent> where TEvent : RabbitMQ.BaseEvent
    {
        void Handle(TEvent @event);
    }
}
