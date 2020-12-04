using AutoMapper;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCatalog.Business.Services
{
    public class CarCatalogService : ICarCatalogService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CarCatalogService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TModel> CreateAsync<TModel>(TModel model)
        {
            var car = _mapper.Map<Car>(model);
            var result = await _mediator.Send(new CreateCarCommand(car));

            return await Task.FromResult(_mapper.Map<TModel>(result));
        }

        public async Task<List<TModel>> GetAllAsync<TModel>()
        {
            var query = new GetAllCarsQuery();
            var results = await _mediator.Send(query);

            return await Task.FromResult(_mapper.Map<List<TModel>>(results));
        }

        public async Task<TModel> GetByIdAsync<TModel>(int id)
        {
            var query = new GetCarByIdQuery(id);
            var result = await _mediator.Send(query);

            return await Task.FromResult(_mapper.Map<TModel>(result));
        }
    }
}
