using CarCatalog.Core.Interfaces.Event;
using System;

namespace CarCatalog.Core.Event.Base
{
    public abstract class Event : IEventBusMessage
    {
        protected Event()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
