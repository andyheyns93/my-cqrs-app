using CarCatalog.Core.Domain;
using CarCatalog.Core.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCatalog.Infrastructure.Repositories.Cache
{
    public class CachedQueryCarCatalogRepository : IQueryCarCatalogRepository
    {
        private readonly IQueryCarCatalogRepository _queryCarCatalogRepository;
        private IMemoryCache _memoryCache;

        public CachedQueryCarCatalogRepository(IQueryCarCatalogRepository queryCarCatalogRepository, IMemoryCache memoryCache)
        {
            _queryCarCatalogRepository = queryCarCatalogRepository;
            _memoryCache = memoryCache;
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            Car cacheEntry;
            var cacheKey = $"{nameof(GetCarByIdAsync)}_{id}";

            if (!_memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = await _queryCarCatalogRepository.GetCarByIdAsync(id);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));
                _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            List<Car> cacheEntry;
            var cacheKey = $"{nameof(GetCarsAsync)}";

            if (!_memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = await _queryCarCatalogRepository.GetCarsAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));
                _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
    }
}
