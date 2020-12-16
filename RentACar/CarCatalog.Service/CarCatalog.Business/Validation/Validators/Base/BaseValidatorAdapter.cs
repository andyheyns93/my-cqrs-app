using CarCatalog.Core.Common.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarCatalog.Business.Validation.Validators.Base
{
    public abstract class BaseValidatorAdapter<IValidatableEntity> : IDisposable
    {
        private bool _disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
            }

            _disposed = true;
        }

        public ValidationResult MergeResults(ValidationResult original, ValidationResult newResult)
        {
            if (original is null)
                return newResult;
            if (!(newResult is null) && !newResult.IsValid)
                return new ValidationResult(original.Errors.Concat(newResult.Errors).Distinct().Distinct());
            return original;
        }

        public ValidationResult MergeResults(ValidationResult original, List<ValidationResult> newResults)
        {
            newResults.ForEach(item => original = MergeResults(original, item));
            return original;
        }
    }
}
