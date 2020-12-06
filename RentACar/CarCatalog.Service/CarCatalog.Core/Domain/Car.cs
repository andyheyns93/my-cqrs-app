using CarCatalog.Core.Interfaces.Domain;
using System;

namespace CarCatalog.Core.Domain
{
    public class Car : IAggregateEntity
    {
        public Car()
        {
        }

        private Car(string brand, string model, int year)
        {
            Id = Guid.NewGuid();
            Brand = brand;
            Model = model;
            Year = year;
        }

        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public static Car CreateNewCar(string brand, string model, int year)
        {
            return new Car(brand, model, year);
        }
    }
}
