using CarCatalog.Api.Contracts.Interfaces;
using CarCatalog.Business.Commands.Base;
using System;

namespace CarCatalog.Business.Commands.Results
{
    public class CreateCommandResult<T> : CommandResult where T : IModel
    {
        public CreateCommandResult()
        {
            Success = false;
        }

        public CreateCommandResult(T model, bool success)
        {
            Id = model.Id.GetValueOrDefault();
            Data = model;
            Success = success;
        }

        public T Data { get; }
    }
}
