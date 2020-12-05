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
        public static void UseExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(_ =>
            {
                _.Run(async context =>
               {
                   context.Response.StatusCode = 500;
                   context.Response.ContentType = "text/html";
                   await context.Response.WriteAsync("Internal Server Error", Encoding.UTF8);
               });
            });
        }
    }
}
