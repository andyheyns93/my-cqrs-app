using System;

namespace CarCatalog.Core.Interfaces.Commands.Results
{
    public interface ICommandResult<T>
    {
        T Data { get; set; }
        bool Success { get; set; }
        DateTime Executed { get; set; }
    }
}
