using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalog.Core.Common.Validation
{
    public class ValidationFailure
    {
        public ValidationFailure(string propertyName, string errorMessage, object attemptedValue)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            AttemptedValue = attemptedValue;
        }

        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public object AttemptedValue { get; set; }
    }
}
