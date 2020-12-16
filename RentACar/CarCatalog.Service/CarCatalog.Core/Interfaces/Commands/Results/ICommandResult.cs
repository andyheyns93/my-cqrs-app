using CarCatalog.Core.Common.Validation;
using System;
using System.Collections.Generic;

namespace CarCatalog.Core.Interfaces.Commands.Results
{
    public interface ICommandResult<T>
    {
        T Data { get; set; }
        bool Success { get; set; }
        DateTime? Executed { get; set; }

        IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
