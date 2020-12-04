using CarCatalog.Core.Interfaces.Repositories.Base;

namespace CarCatalog.Infrastructure.Base
{
    public class CommandDbClient : DbClient, ICommandDbClient
    {
        public CommandDbClient(string connectionString) : base(connectionString)
        {
        }
    }
}
