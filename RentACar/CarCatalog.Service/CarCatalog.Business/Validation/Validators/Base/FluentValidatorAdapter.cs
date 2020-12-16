using CarCatalog.Core.Common.Validation;
using CarCatalog.Core.Interfaces.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCatalog.Business.Validation.Validators.Base
{
    public class FluentValidatorAdapter<T> : BaseValidatorAdapter<T>, IValidator<T>
    {
        private readonly FluentValidation.IValidator<T> _fluentValidator;

        public FluentValidatorAdapter(FluentValidation.IValidator<T> fluentValidator)
        {
            _fluentValidator = fluentValidator;
        }

        public virtual Task<ValidationResult> ValidateAsync(T instance)
        {
            var tcs = new TaskCompletionSource<ValidationResult>();
            _fluentValidator.ValidateAsync(instance).ContinueWith(t =>
            {
                if (t.IsFaulted)
                    tcs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(new FluentValidationResultAdapter(t.Result));
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        public async virtual Task<ValidationResult> ValidateAsync(IEnumerable<T> instances)
        {
            if (instances is null || !instances.Any())
                return new ValidationResult();
            var result = new ValidationResult();
            foreach (var instance in instances)
            {
                result = MergeResults(result, await ValidateAsync(instance));
            }
            return result;
        }

        ~FluentValidatorAdapter()
        {
            Dispose(false);
        }
    }
}
