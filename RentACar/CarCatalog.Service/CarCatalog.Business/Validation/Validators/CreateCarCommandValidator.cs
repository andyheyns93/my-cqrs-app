using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Validation.Fluent;
using CarCatalog.Business.Validation.Validators.Base;
using CarCatalog.Core.Common.Validation;
using System.Threading.Tasks;

namespace CarCatalog.Business.Validation.Validators
{
    public class CreateCarCommandValidator : FluentValidatorAdapter<CreateCarCommand>
    {
        public CreateCarCommandValidator() : base(new CreateCarCommandFluentValidator())
        {
        }

        public override async Task<ValidationResult> ValidateAsync(CreateCarCommand instance)
        {
            if (instance is null)
                return new ValidationResult();

            return await base.ValidateAsync(instance);
        }
    }
}
