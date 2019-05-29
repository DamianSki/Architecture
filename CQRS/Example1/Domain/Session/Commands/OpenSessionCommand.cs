using System;

namespace Domain.Session.Commands
{
    public class OpenSessionCommand
    {
        public Guid Id;
        public DateTime Opened { get; set; }
    }
}