namespace CarCatalog.Core.Interfaces.Validation
{
    public interface IValidatorFactory
    {
        IValidator<T> Create<T, TValidator>(params object[] parameters) where TValidator : IValidator<T>;
    }
}
