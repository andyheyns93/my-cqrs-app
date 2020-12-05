using CarCatalog.Business.Commands;
using CarCatalog.Business.Commands.Base;
using FluentValidation;

namespace CarCatalog.Business.Validation
{
    public class CommandFluentValidator<T> : AbstractValidator<Command<T>>
    {
        public CommandFluentValidator()
        {
            RuleFor(x => x.Payload).NotEmpty();
        }
    }
}
