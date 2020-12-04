using CarCatalog.Core.Interfaces.Commands;

namespace CarCatalog.Business.Commands.Base
{
    public class Command<T> : IPayLoad<T>
    {
        public T Payload { get; set; }

        public Command(T payload)
        {
            Payload = payload;
        }
    }
}
