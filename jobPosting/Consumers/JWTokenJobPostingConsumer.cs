using MassTransit;
using SharedContent.Messages;
using jobPosting.Utils;

namespace jobPosting.Consumers
{
    public class JWTokenJobPostingConsumer : IConsumer<JWTokenJobPosting>
    {
        public async Task Consume(ConsumeContext<JWTokenJobPosting> context)
        {
            try
            {
                var message = context.Message;
                TokenManager.TokenString = message.TokenString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consuming message: {ex.Message}");
                // It's better to return a faulted task instead of just creating one and not using it
                await Task.FromException(ex);
            }
        }
    }
}
