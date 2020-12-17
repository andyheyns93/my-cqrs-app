using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarCatalog.Api.Contracts.Models;
using CarCatalog.Api.Controllers.Base;
using CarCatalog.Business.Commands;
using CarCatalog.Business.Queries;
using CarCatalog.Core.Interfaces.Commands.Results;
using CarCatalog.Core.Interfaces.Queries.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;

namespace RentACar.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class CarCatalogController : BaseController
    {
        private readonly IMediator _mediator;

        public CarCatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all cars
        /// </summary>
        /// <returns>All cars</returns>
        /// <response code="200">Returns all cars in the catalog</response>
        [HttpGet]
        [ProducesResponseType(typeof(IQueryResult<IEnumerable<CarModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            Log.Information("CarController: GetById");

            return await ExecuteAsync(() => _mediator.Send(new GetAllCarsQuery()));
        }

        /// <summary>
        /// Get a car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A car by id</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the car is not found</response>   
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IQueryResult<CarModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            Log.Information("CarController: GetById");
            using var scope = LogContext.PushProperty("RentACar.RequestId", id);

            return await ExecuteAsync(() => _mediator.Send(new GetCarByIdQuery(id)));
        }

        /// <summary>
        /// Create a car for the catalog
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A newly created car</returns>
        /// <response code="200">Returns the newly created car</response>
        /// <response code="400">If the car is null</response>   
        [HttpPost]
        [ProducesResponseType(typeof(ICommandResult<CarModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CarModel car)
        {
            Log.Information("CarController: Create");
            using var scope = LogContext.PushProperty("RentACar.RequestBody", car);

            return await ExecuteAsync(() => _mediator.Send(new CreateCarCommand(car)));
        }
    }
}
