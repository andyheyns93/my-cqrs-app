using CarCatalog.Core.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarCatalog.Core.Common.Validation
{
    [Serializable]
    public class ValidationResult
    {
        private readonly List<ValidationFailure> _errors = new List<ValidationFailure>();

        public ValidationResult()
        {
        }

        public ValidationResult(ValidationFailure failure) : this(new ValidationFailure[] { failure })
        {
        }

        public ValidationResult(IEnumerable<ValidationFailure> errors)
        {
            _errors = new List<ValidationFailure>(errors);
        }

        public virtual IList<ValidationFailure> Errors { get { return _errors; } }

        public virtual bool IsValid { get { return !_errors.Any(); } }
    }
}
