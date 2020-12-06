﻿using CarCatalog.Core.Interfaces.Event;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.EventBus
{
    public interface IEventBus
    {
        Task Publish(IEvent @event);

        Task Subscribe<T>() where T : IEvent;
    }
}
