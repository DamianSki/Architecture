using System;
using System.Collections;

namespace Infrastructure
{
    public abstract class Aggregate
    {
        public Guid Guid { get; private set; }

        private int EventsLoaded { get; set; }

        public void ApplyEvents(IEnumerable events)
        {
            foreach (var @event in events)
            {
                GetType().GetMethod(nameof(ApplyOneEvent)).MakeGenericMethod(@event.GetType())
                    .Invoke(this, new[] {@event});
            }
        }

        public void ApplyOneEvent<TEvent>(TEvent @event)
        {
            var applier = this as IApplyEvent<TEvent>;
            
            if(applier == null)
                throw new InvalidOperationException($"Aggregate {GetType().Name} does not know how to apply event {@event.GetType().Name}");
            
            applier.Apply(@event);
            EventsLoaded++;
        }
    }
}