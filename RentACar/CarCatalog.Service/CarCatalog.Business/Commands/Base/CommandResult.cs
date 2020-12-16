using CarCatalog.Api.Contracts.Interfaces;
using CarCatalog.Core.Common.Validation;
using CarCatalog.Core.Interfaces.Commands.Results;
using System;
using System.Collections.Generic;

namespace CarCatalog.Business.Commands.Base
{
    public class CommandResult<T> : ICommandResult<T> where T : IModel
    {
        public CommandResult(bool success)
        {
            Success = success;
        }

        public CommandResult(bool success, IEnumerable<ValidationFailure> errors)
        {
            Success = success;
            Errors = errors;
        }

        public CommandResult(T model, bool success)
        {
            Id = model.Id.GetValueOrDefault();
            Data = model;
            Success = success;
            Executed = DateTime.UtcNow;
        }

        public T Data { get; set; }

        public Guid? Id { get; set; }

        public bool Success { get; set; }

        public DateTime? Executed { get; set; }

        public IEnumerable<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();
    }
}
