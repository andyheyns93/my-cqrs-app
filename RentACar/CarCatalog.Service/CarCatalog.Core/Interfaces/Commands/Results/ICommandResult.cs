using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalog.Core.Interfaces.Commands.Results
{
    public interface ICommandResult
    {
        bool Success { get; }
        DateTime Executed { get; }
    }
}
