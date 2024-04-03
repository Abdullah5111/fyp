

using RabbitMQ;

namespace ApplyForJob.Services
{
    public class TokenReceiverHandler : ITokenReceiverHandler<ApplyForJobJwtEvent>
    {
        private readonly RabbitMQ.RabbitMQ _rabbitMQ;
        private string _jwtToken;
        public TokenReceiverHandler(RabbitMQ.RabbitMQ rabbitMQ)
        {
            _rabbitMQ = rabbitMQ;
        }

        public void Handle(ApplyForJobJwtEvent applyForJobJwtEvent)
        {
            //Console.WriteLine($"The token is not called yet{applyForJobJwtEvent.JwtToken}\n");
            _jwtToken = applyForJobJwtEvent.JwtToken;
            Console.WriteLine($"The token is {_jwtToken}\n");
        }
        
    }
}
