using CarCatalog.Core.Interfaces.Commands;

namespace CarCatalog.Business.Commands.Base
{
    public abstract class Command<TModel> : ICommand<TModel>
    {
        public TModel Payload { get; set; }

        protected Command(TModel payload)
        {
            Payload = payload;
        }
        public abstract string Name
        {
            get;
        }
    }
}
