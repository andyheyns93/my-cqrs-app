using AutoMapper;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Core.Domain;

namespace CarCatalog.Api.Profiles
{
    public class CarCatalogProfile : Profile
    {
        public CarCatalogProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
        }
    }
}
