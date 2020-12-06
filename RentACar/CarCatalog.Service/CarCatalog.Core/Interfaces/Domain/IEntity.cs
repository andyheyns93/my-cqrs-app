using System;

namespace CarCatalog.Core.Interfaces.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
