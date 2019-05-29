using System;

namespace Domain.Session.Events
{
    public class DealCreatedEvent
    {
        public Guid Id;
        public DateTime Created { get; set; }
    }
}