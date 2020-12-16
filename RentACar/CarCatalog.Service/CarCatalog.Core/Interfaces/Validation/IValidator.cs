using CarCatalog.Core.Common.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCatalog.Core.Interfaces.Validation
{
    public interface IValidator<IValidatableEntity> : IDisposable
    {
        Task<ValidationResult> ValidateAsync(IValidatableEntity instance);
        Task<ValidationResult> ValidateAsync(IEnumerable<IValidatableEntity> instances);
    }
}
