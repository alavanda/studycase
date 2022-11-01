using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Features.Products.Queries;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        private readonly ICatalogContext _catalogContext;
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly ILogger<ProductQueryRepository> _logger;
        private readonly IDatabase _database;

        public ProductQueryRepository(
            ICatalogContext catalogContext,
            ICategoryQueryRepository categoryQueryRepository,
            ILogger<ProductQueryRepository> logger,
            ConnectionMultiplexer redis
            )
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
            _categoryQueryRepository = categoryQueryRepository ?? throw new ArgumentNullException(nameof(categoryQueryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _database = redis.GetDatabase();
        }

        public async Task<GetProductQueryResponse> GetProduct(string id, bool fromCache = true)
        {
            if (fromCache)
            {
                var data = await _database.StringGetAsync(id);

                if (!data.IsNullOrEmpty)
                {
                    return JsonSerializer.Deserialize<GetProductQueryResponse>(data);
                }
            }

            var productProjection = Builders<Product>.Projection
                .Include(x => x.Id)
                .Include(x => x.Name)
                .Include(x => x.Description)
                .Include(x => x.CategoryId)
                .Include(x => x.Price)
                .Include(x => x.Currency);

            var product = await _catalogContext.Products.Find(x => x.Id == id).Project<Product>(productProjection).FirstOrDefaultAsync();

            if (product == null)
            {
                _logger.LogInformation("Böyle bir ürün yok.");

                return null;
            }

            if (string.IsNullOrEmpty(product.CategoryId))
            {
                _logger.LogInformation("ProductId : {@productId} için CategoryId yok.", id);

                return null;
            }

            var category = await _categoryQueryRepository.GetCategory(product.CategoryId);

            if (category == null)
            {
                _logger.LogInformation("CategoryId : {@categoryId} için kategori yok.", product.CategoryId);

                return null;
            }

            var response = new GetProductQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = category,
                Price = product.Price,
                Currency = product.Currency
            };

            if (fromCache)
            {
                var created = await _database.StringSetAsync(id, JsonSerializer.Serialize(response), TimeSpan.FromSeconds(300));

                if (!created)
                {
                    _logger.LogError("Redise yazarken hata oluştu.");
                }
            }

            return response;
        }
    }
}
