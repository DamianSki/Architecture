using System;

namespace Domain.Session.Commands
{
    public class CloseSessionCommand
    {
        public Guid Id { get; set; }
        public DateTime Closed { get; set; }
    }
}
