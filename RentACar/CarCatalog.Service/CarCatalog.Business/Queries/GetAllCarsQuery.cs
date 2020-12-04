using CarCatalog.Core.Domain;
using MediatR;
using System.Collections.Generic;

namespace CarCatalog.Business.Queries
{
    public class GetAllCarsQuery : IRequest<List<Car>>
    {
    }
}
