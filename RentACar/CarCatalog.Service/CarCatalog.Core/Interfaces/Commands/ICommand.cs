namespace CarCatalog.Core.Interfaces.Commands
{
    public interface ICommand<T>
    {
        T Payload { get; set; }
    }
}
