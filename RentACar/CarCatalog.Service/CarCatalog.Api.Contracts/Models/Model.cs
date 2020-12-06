using CarCatalog.Api.Contracts.Interfaces;
using System;

namespace CarCatalog.Api.Contracts.Models
{
    public class Model : IModel
    {
        public Guid Id { get; set; }
    }
}
