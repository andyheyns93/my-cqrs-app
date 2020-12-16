using CarCatalog.Business.Commands.Base;
using CarCatalog.Core.Interfaces.Commands;
using FluentValidation;

namespace CarCatalog.Business.Validation.Base
{
    public class CommandFluentValidator<T, U> : AbstractValidator<T> where T : ICommand<U>
    {
        public CommandFluentValidator()
        {
            RuleFor(x => x.Payload).NotEmpty();
        }
    }
}
