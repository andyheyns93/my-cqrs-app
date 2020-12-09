using CarCatalog.Core.Interfaces.Commands.Results;
using CarCatalog.Core.Interfaces.Queries.Results;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CarCatalog.Api.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<ICommandResult<T>>> controllerAction)
        {
            try
            {
                return HandleResponse(await controllerAction());
            }
            catch (Exception ex)
            {
                var result = HandleException(ex);
                return result;
            }

        }

        protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<IQueryResult<T>>> controllerAction)
        {
            try
            {
                return HandleResponse(await controllerAction());
            }
            catch (Exception ex)
            {
                var result = HandleException(ex);
                return result;
            }
        }

        private IActionResult HandleResponse<T>(ICommandResult<T> response)
        {
            if (response.Data is null)
            {
                Log.Error("Response is empty");
                return BadRequest();
            }
            return Ok(response);
        }

        private IActionResult HandleResponse<T>(IQueryResult<T> response)
        {
            if (response.Data is null)
            {
                Log.Error("Response is empty");
                return NotFound();
            }
            return Ok(response);
        }

        private IActionResult HandleException(Exception exception)
        {
            Log.Fatal(exception, "Exception occurred");
            return StatusCode((int)HttpStatusCode.InternalServerError, "Exception occurred");
        }
    }
}
