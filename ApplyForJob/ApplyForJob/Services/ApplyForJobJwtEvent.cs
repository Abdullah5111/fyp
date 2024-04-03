using RabbitMQ;

namespace ApplyForJob.Services
{
    public class ApplyForJobJwtEvent : BaseEvent
    {
        public string JwtToken { get; set; }
    }
}
