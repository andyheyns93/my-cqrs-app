using CarCatalog.Core.Common.Validation;

namespace CarCatalog.Business.Validation.Validators.Base
{
    public class FluentValidationFailureAdapter : ValidationFailure
    {
        public FluentValidationFailureAdapter(FluentValidation.Results.ValidationFailure fluentValidationFailure)
            : base(fluentValidationFailure.PropertyName, fluentValidationFailure.ErrorMessage, fluentValidationFailure.AttemptedValue)
        {
        }
    }
}
