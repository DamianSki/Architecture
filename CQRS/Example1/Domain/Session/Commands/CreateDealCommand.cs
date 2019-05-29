using System;
using System.Collections.Generic;
using Domain.Session.Events.Shared;

namespace Domain.Session.Commands
{
    public class CreateDealCommand
    {
        public Guid Id;
        public DateTime Created { get; set; }
        public IEnumerable<CounterpartyEvent> Counterparties { get; set; }
        public IEnumerable<TransactionEvent> Trasactions { get; set; }
    }
}