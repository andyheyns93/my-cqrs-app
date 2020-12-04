using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalog.Core.Interfaces.Commands
{
    public interface IPayLoad<T>
    {
        T Payload { get; set; }
    }
}
