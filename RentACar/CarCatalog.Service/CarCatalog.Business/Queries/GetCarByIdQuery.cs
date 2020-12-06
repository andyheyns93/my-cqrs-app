﻿using CarCatalog.Business.Queries.Base;
using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Handlers;
using System;

namespace CarCatalog.Business.Queries
{
    public class GetCarByIdQuery : Query, IQueryRequest<Car>
    {
        public GetCarByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }

        public override string Name => "GetCarByIdQuery";
    }
}
