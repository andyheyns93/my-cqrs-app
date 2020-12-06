using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalog.Core.Interfaces.Event
{
    public interface IEvent
    {
        Guid Id { get; set; }
        string Name { get; set; }
        DateTime OccurredOn { get; set; }
    }
}
