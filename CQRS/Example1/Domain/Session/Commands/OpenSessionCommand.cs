using System;
using System.Collections;
using Commands;
using Infrastructure;

namespace Domain.Session.Commands
{
    public class OpenSessionCommand
    {
        public Guid Id;
        public DateTime Opened { get; set; }
        public DateTime Closed { get; set; }
    }
}