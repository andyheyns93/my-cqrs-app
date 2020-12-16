using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarCatalog.Core.Common.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        private readonly List<ValidationFailure> _errors = new List<ValidationFailure>();

        public ValidationException(string message) : base(message)
        {
            Log.Error(this, message);
        }

        public ValidationException(string message, IEnumerable<ValidationFailure> errors) : base(message)
        {
            _errors = new List<ValidationFailure>(errors);
        }

        public IEnumerable<ValidationFailure> Errors { get { return _errors; } }
    }
}
