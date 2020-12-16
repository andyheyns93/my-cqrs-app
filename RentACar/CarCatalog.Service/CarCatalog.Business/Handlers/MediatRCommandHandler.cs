using CarCatalog.Core.Common.Validation;
using CarCatalog.Core.Interfaces.Validation;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers
{
    public abstract class MediatRCommandHandler
    {
        protected readonly List<ValidationFailure> _errors = new List<ValidationFailure>();
        protected async Task ValidateAsync<T>(T instance, IValidator<T> validator, bool throwException = true)
        {
            ValidationResult validationResult = await validator.ValidateAsync(instance);
            if (!validationResult.IsValid)
            {
                if (throwException)
                {
                    var messages = JsonConvert.SerializeObject(validationResult.Errors);
                    Log.Information($"Valdiation Failed: {messages}");

                    throw new ValidationException("Validation Exception", validationResult.Errors);
                }
                _errors.AddRange(validationResult.Errors);
            }
        }
    }
}
