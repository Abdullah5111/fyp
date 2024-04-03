using Microsoft.Extensions.Logging;

namespace Authentication.EventPublishing.EventHandeler
{
    public interface IPublishTokenEventHandler<TEvent> where TEvent : RabbitMQ.BaseEvent
    {
        Task Handle(TEvent @event);
    }
}
