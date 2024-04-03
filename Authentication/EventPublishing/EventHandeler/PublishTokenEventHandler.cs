
using RabbitMQ;

namespace Authentication.EventPublishing.EventHandeler
{
    public class PublishTokenEventHandler : IPublishTokenEventHandler<PublishToken>
    {
        private readonly RabbitMQ.RabbitMQ _rabbitMQ;

        public PublishTokenEventHandler(RabbitMQ.RabbitMQ rabbitMQ)
        {
            _rabbitMQ = rabbitMQ;
        }

        public async Task Handle(PublishToken publishToken)
        {
            var eventEnvelope = new EventEnvelop
            {
                EventType = "PublishToken",
                EventData = publishToken
            };
            await _rabbitMQ.PublishMessage("jwtCarrierExchange", eventEnvelope, "jwtCarrier");
        }
    }
}
