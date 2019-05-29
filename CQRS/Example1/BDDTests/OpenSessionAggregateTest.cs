using System;
using BDDTests.Pirmitives;
using Domain.Session;
using Domain.Session.Commands;
using Domain.Session.Events;
using Xunit;

namespace BDDTests
{
    public class SessionAggregateTest : BaseBddTests<SessionAggregate>
    {
        private Guid _id;
        private DateTime _opened;
        
        public SessionAggregateTest()
        {
            _id = new Guid();
            _opened = DateTime.Now;
        }
        
        [Fact]
        public void OpenSession()
        {
            Given()
            .When(new OpenSessionCommand
            {
                Id = _id,
                Opened = _opened,                
            })
            .Then(new SessionOpenedEvent
            {
                Id = _id,
                Opened = _opened,
            });
        }

        [Fact]
        public void CloseSession()
        {
            var dt = DateTime.Now;

            Given(new SessionOpenedEvent {
                Opened = DateTime.Now.Add(new TimeSpan(-1))
            })
            .When(new CloseSessionCommand {
                Id = _id,
                Closed = dt
            })
            .Then(new CloseSessionEvent {
                Id = _id,
                Closed =  dt
            });
        }
    }
}