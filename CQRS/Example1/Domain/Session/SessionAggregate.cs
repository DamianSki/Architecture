using System.Collections;
using Commands;
using Domain.Session.Commands;
using Events;
using Infrastructure;

namespace Domain.Session
{
    public class SessionAggregate : Aggregate, IHandleCommand<OpenSessionCommand>, IHandleCommand<CreateDealCommand>
    {
        public IEnumerable Handle(OpenSessionCommand command)
        {
            yield return new SessionOpenedEvent
            {
                Id = command.Id
            };
        }

        public IEnumerable Handle(CreateDealCommand command)
        {
            yield return new DealCreatedEvent
            {
                Id = command.Id
            };
        }
    }
}