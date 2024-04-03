namespace Resume.RabbitMQ
{
    public interface IResumeIdProducer
    {
        public void SendResumeIdMessage<T>(T message);
    }
}
