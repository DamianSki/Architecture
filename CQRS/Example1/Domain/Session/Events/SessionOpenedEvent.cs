using System;

namespace Domain.Session.Events
{
    public class SessionOpenedEvent
    {
        public Guid Id;
        public DateTime Opened { get; set; }
    }
}