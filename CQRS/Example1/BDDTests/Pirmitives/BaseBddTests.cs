using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Infrastructure;
using Infrastructure.Exceptions;
using Xunit;

namespace BDDTests.Pirmitives
{
    public abstract class BaseBddTests<TAggregate> where TAggregate : Aggregate, new()
    {
        private TAggregate Aggregate { get; }
        private IEnumerable Events { get; set; }

        protected BaseBddTests()
        {
            Aggregate = new TAggregate();
        }

        protected void Test(IEnumerable given, Func<TAggregate, object> when, Action<object> then)
        {
            then(when(ApplyEvents(Aggregate, given)));
        }

        private TAggregate ApplyEvents(TAggregate aggregate, IEnumerable @events)
        {
            aggregate.ApplyEvents(@events);
            return aggregate;
        }

        public BaseBddTests<TAggregate> Given(params object[] @events)
        {
            ApplyEvents(Aggregate, events);

            return this;
        }

        public BaseBddTests<TAggregate> When<TCommand>(TCommand command)
        {
            Events = DispatchCommand(command).Cast<object>().ToArray();

            return this;
        }

        public BaseBddTests<TAggregate> Then(params object[] expectedEvents)
        {
            var events = (object[]) Events;
            
            if (events != null)
            {
                if (events.Length == expectedEvents.Length)
                {
                    for (int i = 0; i < events.Length; i++)
                    {
                        if (events[i].GetType() == expectedEvents[i].GetType())
                        {
                            Assert.Equal(Serialize(expectedEvents[i]), Serialize(events[i]));
                        }
                        else {
                            Assert.True(false, $"Incorrect event in results; expected a {expectedEvents[i].GetType().Name} but got a {events[i].GetType().Name}");
                        }
                    }       
                }
                else if (events.Length < expectedEvents.Length)
                {
                    Assert.True(false, $"Expected event(s) missing: {string.Join(", ", EventDiff(expectedEvents, events))}");
                }
                else
                {
                    Assert.True(false, $"Unexpected event(s) emitted: {string.Join(", ", this.EventDiff(expectedEvents, events))}");
                }
            }            

            return this;
        }
        
        protected BaseBddTests<TAggregate> ThenFailWith<TException>(Exception ex)
        {
            switch (ex)
            {
                case TException _:
                    Assert.True(true, "Got correct exception type");
                    break;
                case CommandHandlerNotDefException exception:
                    Assert.False(true, exception.Message);
                    break;
                case Exception _:
                    Assert.False(true, $"Expected exception {typeof(TException).Name}, but got exception {ex.GetType().Name}");
                    break;
                default:
                    Assert.False(true, $"Expected exception {typeof(TException).Name}, but got event result");
                    break;
            }

            return this;
        }

        private IEnumerable DispatchCommand<TCommand>(TCommand command)
        {
            var handler = Aggregate as IHandleCommand<TCommand>;

            if (handler == null)
            {
                throw new CommandHandlerNotDefException($"Aggregate {Aggregate.GetType().Name} does not yet handle command {command.GetType().Name}");   
            }

            return handler.Handle(command);
        }
        
        private string Serialize(object obj)
        {
            var serialized = new XmlSerializer(obj.GetType());
            var ms = new MemoryStream();
            serialized.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return new StreamReader(ms).ReadToEnd();
        }

        private string[] EventDiff(object[] a, object[] b)
        {
            var diff = a.Select(e => e.GetType().Name).ToList();

            foreach (var toRemove in b.Select(e => e.GetType().Name))
                diff.Remove(toRemove);
            
            return diff.ToArray();
        }
    }
}