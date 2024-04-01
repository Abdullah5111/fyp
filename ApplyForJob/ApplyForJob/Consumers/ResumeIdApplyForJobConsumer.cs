using ApplyForJob.Utils;
using MassTransit;
using SharedContent.Messages;

namespace ApplyForJob.Consumers
{
    public class ResumeIdApplyForJobConsumer: IConsumer<ResumeId>
    {
        public async Task Consume(ConsumeContext<ResumeId> context)
        {
            try
            {
                var message = context.Message;
                UserResumeId.ResumeId = message.UserResumeId;
                Console.WriteLine("Resume id consumed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consuming message: {ex.Message}");
                Task.FromException(ex);
            }
        }
    }
}
