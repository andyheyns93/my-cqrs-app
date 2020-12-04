using CarCatalog.Business.Commands;
using FluentValidation;

namespace CarCatalog.Business.Validation
{
    public class CreateCarCommandValidator: AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(x => x.Payload).NotEmpty();
        }
    }
}
