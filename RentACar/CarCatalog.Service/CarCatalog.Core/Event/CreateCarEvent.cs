using CarCatalog.Core.Domain;
using MediatR;

namespace CarCatalog.Core.Event
{
    public class CreateCarEvent : Base.Event, IRequest<Unit>
    {
        public Car Data { get; set; }
        public CreateCarEvent(Car car)
        {
            Data = car;
            Name = (nameof(CreateCarEvent));
        }
    }
}
