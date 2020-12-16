using CarCatalog.Core.Common.Validation;
using System.Linq;

namespace CarCatalog.Business.Validation.Validators.Base
{
    public class FluentValidationResultAdapter : ValidationResult
    {
        public FluentValidationResultAdapter(FluentValidation.Results.ValidationResult fluentValidationResult)
            : base(fluentValidationResult.Errors.Select(f => new FluentValidationFailureAdapter(f)))
        {
        }
    }
}
