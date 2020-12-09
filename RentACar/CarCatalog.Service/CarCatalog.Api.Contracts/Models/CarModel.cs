using System.ComponentModel.DataAnnotations;

namespace CarCatalog.Api.Contracts.Models
{
    public class CarModel : Model
    {
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int? Year { get; set; }
    }
}
