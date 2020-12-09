using CarCatalog.Api.Contracts.Interfaces;
using CarCatalog.Core.Interfaces.Commands.Results;
using System;

namespace CarCatalog.Business.Commands.Base
{
    public class CommandResult<T> : ICommandResult<T> where T : IModel
    {
        public CommandResult(T model, bool success)
        {
            Id = model.Id.GetValueOrDefault();
            Data = model;
            Success = success;
            Executed = DateTime.UtcNow;
        }

        public T Data { get; set; }

        public Guid Id { get; set; }

        public bool Success { get; set; }

        public DateTime Executed { get; set; }
    }
}
