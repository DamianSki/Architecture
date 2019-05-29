using System.Collections;
using Domain.Session.Commands;
using Domain.Session.Events;
using Infrastructure;

namespace Domain.Session
{
    public class SessionAggregate : Aggregate, IHandleCommand<OpenSessionCommand>, IHandleCommand<CreateDealCommand>, IHandleCommand<CloseSessionCommand>, IApplyEvent<SessionOpenedEvent>
    {
        private bool _active;

        public IEnumerable Handle(OpenSessionCommand command)
        {
            yield return new SessionOpenedEvent {Id = command.Id, Opened = command.Opened};
        }

        public IEnumerable Handle(CreateDealCommand command)
        {
            yield return new DealCreatedEvent {Id = command.Id};
        }

        public IEnumerable Handle(CloseSessionCommand command)
        {
            yield return new CloseSessionEvent { Id = command.Id, Closed = command.Closed };
        }

        public void Apply(SessionOpenedEvent @event)
        {
            _active = true;
        }
    }
}