using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class BaseEvent
    {
        public string EventId { get; set; }
        public DateTime Timestamp { get; set; }

        public BaseEvent()
        {
            EventId = Guid.NewGuid().ToString();
            Timestamp = DateTime.UtcNow;
        }
        public BaseEvent(string EventId, DateTime Timestamp)
        {
            this.EventId = EventId;
            this.Timestamp = Timestamp;

        }
    }
}
