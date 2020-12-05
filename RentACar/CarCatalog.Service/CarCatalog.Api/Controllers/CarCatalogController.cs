using System.Threading.Tasks;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace RentACar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarCatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarCatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Log.Information("CarController: GetById");

            var data = await _mediator.Send(new GetAllCarsQuery());
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Information("CarController: GetById");

            var data = await _mediator.Send(new GetCarByIdQuery(id));
            return data != null ? (IActionResult)Ok(data) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarDto car)
        {
            Log.Information("CarController: Create");

            var data = await _mediator.Send(new CreateCarCommand(car));
            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }
    }
}
