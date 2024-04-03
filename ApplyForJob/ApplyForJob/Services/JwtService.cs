using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ;
using System;
using Microsoft.Extensions.Hosting;
namespace ApplyForJob.Services
{
    public class JwtService
    {
        private readonly RabbitMQ.RabbitMQ _rabbitMQ;
        private readonly ITokenReceiverHandler<ApplyForJobJwtEvent> _jwtEventHandler;

        public JwtService(RabbitMQ.RabbitMQ rabbitMQConnection, ITokenReceiverHandler<ApplyForJobJwtEvent> jwtEventHandler)
        {
            _rabbitMQ = rabbitMQConnection;
            _jwtEventHandler = jwtEventHandler;
        }
        public void StartConsumingJwts()
        {
            _rabbitMQ.ConsumeMessage<EventEnvelop>("ApplyForJobJwt", (envelope) =>
            {
                if (envelope.EventType == "PublishToken")
                {
                    var serializedEventData = JsonConvert.SerializeObject(envelope.EventData);
                    var eventDataObject = JObject.Parse(serializedEventData);
                    var jwtToken = eventDataObject["JWTToken"].ToString();
                    var eventId = eventDataObject["EventId"].ToString();
                    var timestamp = DateTime.Parse(eventDataObject["Timestamp"].ToString());
                    var applyForJobJwt = new ApplyForJobJwtEvent
                    {
                        JwtToken = jwtToken,
                        EventId = eventId,
                        Timestamp = timestamp,
                    };

                    if (applyForJobJwt != null)
                    {
                        _jwtEventHandler.Handle(applyForJobJwt); 
                    }
                    else
                    {
                        Console.WriteLine("Event not recived \n");
                    }
                }
                else
                {
                    Console.WriteLine("Event Not Of Type\n");
                }
            });
        }
    }
}
