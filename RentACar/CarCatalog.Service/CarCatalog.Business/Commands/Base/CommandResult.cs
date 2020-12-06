using CarCatalog.Core.Interfaces.Commands.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarCatalog.Business.Commands.Base
{
    public abstract class CommandResult : ICommandResult
    {
        protected CommandResult()
        {
            Success = false;
            Executed = DateTime.Now;
        }
        protected CommandResult(bool success)
        {
            Success = success;
            Executed = DateTime.Now;
        }

        public Guid Id { get; set; }

        public bool Success { get; set; }

        public DateTime Executed { get; set; }

        public static TCommandResult CreateFailResult<TCommandResult>() where TCommandResult : CommandResult, new()
        {
            return new TCommandResult();
        }
    }
}
