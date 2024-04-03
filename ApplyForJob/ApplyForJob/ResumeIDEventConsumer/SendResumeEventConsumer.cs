using EventBus.Events;
using MassTransit;

namespace ApplyForJob.ResumeIDEventConsumer
{
    public class SendResumeEventConsumer : IConsumer<SendResumeEvent>
    {
        private static int ResumeID=0;
        public async Task Consume(ConsumeContext<SendResumeEvent> context)
        {
            var targetConsumer = "consumer1";
            if (context.Message.TargetConsumer == targetConsumer)
            {
                int resumeId = context.Message.rResumeId;
                ResumeID = resumeId;

                await Console.Out.WriteLineAsync($"Received SendResumeEvent with ResumeId: {context.Message.rResumeId}");
                
            }
            else
            {
                await Console.Out.WriteLineAsync($"Ignoring SendResumeEvent with ResumeId: {context.Message.rResumeId} because it's not targeted for this consumer.");
            }
        }
        public static int GetId()
        {
            return ResumeID;
        }
    }
}
