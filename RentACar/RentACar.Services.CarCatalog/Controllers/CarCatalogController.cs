using System.Threading.Tasks;
using CarCatalog.Api.Models;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace RentACar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarCatalogController : ControllerBase
    {
        private readonly ICarCatalogService _carCatalogService;

        public CarCatalogController(ICarCatalogService carService)
        {
            _carCatalogService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Log.Information("CarController: GetById");

            var data = await _carCatalogService.GetAllAsync<CarDto>();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Information("CarController: GetById");

            var data = await _carCatalogService.GetByIdAsync<CarDto>(id);
            return data != null ? (IActionResult)Ok(data) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarDto car)
        {
            Log.Information("CarController: Create");

            var data = await _carCatalogService.CreateAsync(car);
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }
    }
}
