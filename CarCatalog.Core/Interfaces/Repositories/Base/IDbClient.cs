using System.Data;

namespace CarCatalog.Core.Interfaces.Repositories.Base
{
    public interface IDbClient
    {
        IDbConnection GetConnection();
    }
}
