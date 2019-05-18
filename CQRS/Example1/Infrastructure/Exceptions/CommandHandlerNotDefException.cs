using System;

namespace Infrastructure.Exceptions
{
    public class CommandHandlerNotDefException : Exception
    {
        public CommandHandlerNotDefException(string msg) : base(msg)
        {
            
        }
    }
}