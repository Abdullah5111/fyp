using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace Resume.RabbitMQ
{
    public class ResumeIdProducer : IResumeIdProducer, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ResumeIdProducer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare("ResumeId", exclusive: false);
        }

        public void SendResumeIdMessage<T>(T message)
        {
            try
            {
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                _channel.BasicPublish(exchange: "", routingKey: "resumeId", body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }

}
