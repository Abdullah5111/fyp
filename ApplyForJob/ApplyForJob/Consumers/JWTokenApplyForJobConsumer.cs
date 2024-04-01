using MassTransit;
using SharedContent.Messages;
using ApplyForJob.Utils;

namespace ApplyForJob.Consumers
{
    public class JWTokenApplyForJobConsumer: IConsumer<JWTokenApplyForJob>
    {
        public async Task Consume(ConsumeContext<JWTokenApplyForJob> context)
        {
            try
            {
                var message = context.Message;
                TokenManager.TokenString = message.TokenString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consuming message: {ex.Message}");
                Task.FromException(ex);
            }
        }
    }
}
