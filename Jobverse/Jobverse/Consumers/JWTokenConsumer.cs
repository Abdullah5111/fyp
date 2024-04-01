using MassTransit;
using SharedContent.Messages;
using Jobverse.Utils;

namespace Jobverse.Consumers
{
    public class JWTokenConsumer: IConsumer<JWToken>
    {
        public async Task Consume(ConsumeContext<JWToken> context)
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
