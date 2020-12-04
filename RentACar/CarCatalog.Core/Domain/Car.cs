using MediatR;
using System;

namespace CarCatalog.Core.Domain
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

    }
}
