using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Features.Categories.Queries;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly ICatalogContext _catalogContext;
        private readonly ILogger<CategoryQueryRepository> _logger;

        public CategoryQueryRepository(ICatalogContext catalogContext, ILogger<CategoryQueryRepository> logger)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GetCategoryQueryResponse> GetCategory(string id)
        {
            var projection = Builders<Category>.Projection.Include(x => x.Id).Include(x => x.Name).Include(x => x.Description);

            var result = await _catalogContext.Categories.Find(x => x.Id == id).Project<Category>(projection).FirstOrDefaultAsync();

            if (result == null)
            {
                _logger.LogInformation("CategoryId : {id} ye sahip bir kategori yok.", id);

                return null;
            }

            return new GetCategoryQueryResponse { Id = result.Id, Name = result.Name, Description = result.Description };
        }
    }
}
