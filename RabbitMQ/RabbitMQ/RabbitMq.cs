using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using System.Threading.Channels;
using Newtonsoft.Json;

namespace RabbitMQ
{
    public class RabbitMQ
    {
        private readonly string _connectionString;
        private readonly string _exchangeName = "jwtCarrierExchange";
        private IConnection _connection;

        public RabbitMQ()
        {
            _connectionString = "amqp://guest:guest@localhost:5672/";
        }

        public IModel CreateModel()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = GetConnection();
            }

            return _connection.CreateModel();
        }

        private IConnection GetConnection()
        {
            var factory = new ConnectionFactory { Uri = new Uri(_connectionString) };
            return factory.CreateConnection();
        }

        public void CreateExchangeIfNotExists(string exchangeName, string exchangeType = "fanout")
        {
            using (var model = CreateModel())
            {
                Console.WriteLine($"Creating exchange: {exchangeName}, type: {exchangeType}");
                model.ExchangeDeclare(exchange: exchangeName,
                                       type: exchangeType,
                                       durable: true);
            }
        }

        public async Task PublishMessage<TEvent>(string exchangeName, TEvent @event, string routingKey)
        {
            Console.WriteLine($"Publishing message to exchange: {exchangeName}, routingKey: {routingKey}");
            Console.WriteLine($"The event type is {typeof(TEvent)}");
            using (var model = CreateModel())
            {
                var body = System.Text.Json.JsonSerializer.Serialize(@event);
                Console.WriteLine($"Serialized message: {body}");
                string exchangeType = routingKey == "jwtCarrier" ? "fanout" : "direct";
                CreateExchangeIfNotExists(exchangeName, exchangeType);
                model.BasicPublish(exchange: exchangeName,
                                    routingKey: routingKey,
                                    basicProperties: null,
                                    body: Encoding.UTF8.GetBytes(body));
            }
        }

        public void ConsumeMessage<TEvent>(string queueName, Action<EventEnvelop> onEventReceived)
        {
            Console.WriteLine($"Consuming messages from queue: {queueName}");
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };

                using (var connection = CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout, durable: true);
                    channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                    channel.QueueBind(queue: queueName, exchange: _exchangeName, routingKey: "jwtCarrier");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received message: {0}", message);
                        try
                        {
                            string eventType = null;
                            object eventData = null;
                            dynamic jsonObject = JsonConvert.DeserializeObject(message);
                            eventType = jsonObject.EventType;
                            eventData = jsonObject.EventData;
                            Console.WriteLine($"The members are {eventType} and {eventData}\n");
                            var eventEnvelope = new EventEnvelop
                            {
                                EventType = eventType,
                                EventData = eventData
                            };
                            onEventReceived(eventEnvelope);
                        }
                        catch (Newtonsoft.Json.JsonException ex)
                        {
                            Console.WriteLine($"Error deserializing message: {ex.Message}");
                        }
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    Console.WriteLine("Consumer started.");
                }
        }

        private IConnection CreateConnection()
        {
            var factory = new ConnectionFactory { Uri = new Uri(_connectionString) };
            return factory.CreateConnection();
        }


        private void BindQueue(IModel channel, string queueName, string exchangeName)
        {
            Console.WriteLine($"Binding queue: {queueName} to exchange: {exchangeName}");
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");
        }
    }

}
