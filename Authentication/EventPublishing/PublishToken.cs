
using RabbitMQ;

namespace Authentication.EventPublishing
{
    public class PublishToken:BaseEvent
    {
        public string JWTToken { get; set; }
    }
}
