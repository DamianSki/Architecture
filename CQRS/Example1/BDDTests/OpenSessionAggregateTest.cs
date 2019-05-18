using System;
using System.Collections;
using BDDTests.Pirmitives;
using Domain.Session;
using Domain.Session.Commands;
using Events;
using Infrastructure;
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
        public void Test1()
        {
            Given()
            .When(new OpenSessionCommand
            {
                Id = _id,
                Opened = DateTime.Now,
                Closed = DateTime.Now.Add(new TimeSpan(1))
            });
            
            Test(
                Given(), 
                When(new OpenSessionCommand
                {
                    Id = _id,
                    Opened = DateTime.Now,
                    Closed = DateTime.Now.Add(new TimeSpan(1))
                }),
                Then(new SessionOpenedEvent
                {
                    Id = _id
                })
            );
        }
    }
}