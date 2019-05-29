using System;

namespace Domain.Session.Events
{
    public class CloseSessionEvent
    {
        public Guid Id { get; set; }
        public DateTime Closed { get; set; }
    }
}