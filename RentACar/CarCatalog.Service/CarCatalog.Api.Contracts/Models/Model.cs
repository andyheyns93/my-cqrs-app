using CarCatalog.Api.Contracts.Attributes;
using CarCatalog.Api.Contracts.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarCatalog.Api.Contracts.Models
{
    public class Model : IModel
    {
        [NotEmptyGuid]
        public Guid? Id { get; set; }
    }
}
