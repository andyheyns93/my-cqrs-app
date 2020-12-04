using System.Linq;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace RentACar
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(_ =>
            {
                _.Run(async context =>
               {
                   var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                   var exception = errorFeature.Error;

                   if(!(exception is ValidationException validationException))
                   {
                       throw exception;
                   }

                   var errors = validationException.Errors.Select(err => new { err.PropertyName, err.ErrorMessage });
                   var errorText = JsonConvert.SerializeObject(errors);

                   context.Response.StatusCode = 400;
                   context.Response.ContentType = "application/json";
                   await context.Response.WriteAsync(errorText, Encoding.UTF8);
               });
            });
        }
    }
}
