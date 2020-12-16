using CarCatalog.Core.Interfaces.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalog.Business.Validation.Validators.Factory
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidatorFactory(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public IValidator<T> Create<T, TValidator>(params object[] parameters) where TValidator : IValidator<T>
        {
            var validator = (IValidator<T>)ActivatorUtilities
                .CreateInstance<TValidator>(_serviceProvider, parameters);
            _httpContextAccessor.HttpContext.Response.RegisterForDispose(validator);
            return validator;
        }
    }
}
