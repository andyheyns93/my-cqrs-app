using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarCatalog.Core.Services
{
    public interface ICarCatalogService
    {
        Task<TModel> CreateAsync<TModel>(TModel model);
        Task<TModel> GetByIdAsync<TModel>(int id);
        Task<List<TModel>> GetAllAsync<TModel>();
    }
}
