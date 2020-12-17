namespace CarCatalog.Core.Interfaces.Queries.Results
{
    public interface IQueryResult<T>
    {
        T Data { get; set; }
    }
}
