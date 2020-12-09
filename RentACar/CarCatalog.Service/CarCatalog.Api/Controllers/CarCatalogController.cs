using System;
using System.Threading.Tasks;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Api.Controllers.Base;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;

namespace RentACar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarCatalogController : BaseController
    {
        private readonly IMediator _mediator;

        public CarCatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Log.Information("CarController: GetById");

            return await ExecuteAsync(() => _mediator.Send(new GetAllCarsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Log.Information("CarController: GetById");
            using var scope = LogContext.PushProperty("RentACar.RequestId", id);

            return await ExecuteAsync(() => _mediator.Send(new GetCarByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarModel car)
        {
            Log.Information("CarController: Create");
            using var scope = LogContext.PushProperty("RentACar.RequestBody", car);

            return await ExecuteAsync(() => _mediator.Send(new CreateCarCommand(car)));
        }
    }
}
