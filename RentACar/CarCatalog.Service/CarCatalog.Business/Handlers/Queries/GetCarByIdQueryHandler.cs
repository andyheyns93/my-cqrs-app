﻿using AutoMapper;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarCatalog.Business.Handlers.Queries
{
    public class GetCarByIdQueryHandler : MediatRHandler, IRequestHandler<GetCarByIdQuery, CarModel>
    {
        private readonly IMapper _mapper;
        private readonly IQueryCarCatalogRepository _queryCarCatalogRepository;

        public GetCarByIdQueryHandler(IMapper mapper, IQueryCarCatalogRepository queryCarCatalogRepository)
        {
            _mapper = mapper;
            _queryCarCatalogRepository = queryCarCatalogRepository;
        }

        public async Task<CarModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var domainObject = await _queryCarCatalogRepository.GetCarByIdAsync(request.Id);
            return await Task.FromResult(_mapper.Map<CarModel>(domainObject));
        }
    }
}
