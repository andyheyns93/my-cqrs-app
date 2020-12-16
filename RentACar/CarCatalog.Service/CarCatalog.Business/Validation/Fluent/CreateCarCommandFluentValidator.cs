using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Validation.Base;

namespace CarCatalog.Business.Validation.Fluent
{
    public class CreateCarCommandFluentValidator : CommandFluentValidator<CreateCarCommand, CarModel>
    {
        public CreateCarCommandFluentValidator() : base()
        {
        }
    }
}
