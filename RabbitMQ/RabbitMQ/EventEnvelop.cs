using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class EventEnvelop
    {
        public string EventType { get; set; }
        public object EventData { get; set; }
    }
}
