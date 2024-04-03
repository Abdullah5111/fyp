using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class SendResumeEvent:IntegrationBaseEvent
    {
        public int rResumeId { get; set; }
        public string TargetConsumer { get; set; }
    }
}
