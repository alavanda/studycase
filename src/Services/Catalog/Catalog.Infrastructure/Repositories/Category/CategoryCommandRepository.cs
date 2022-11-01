using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Features.Categories.Commands;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using System;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryCommandRepository : ICategoryCommandRepository
    {
        private readonly ICatalogContext _catalogContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryCommandRepository> _logger;

        public CategoryCommandRepository(
            ICatalogContext catalogContext,
            IMapper mapper,
            ILogger<CategoryCommandRepository> logger
            )
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> CreateCategory(CreateCategoryCommand command)
        {
            var category = _mapper.Map<Category>(command);

            await _catalogContext.Categories.InsertOneAsync(category);

            if (!string.IsNullOrEmpty(category.Id))
            {
                _logger.LogInformation("Kategori : {@category} başarılı bir şekilde oluşturuldu.", category);
            }

            return category.Id;
        }

        public async Task<bool> UpdateCategory(UpdateCategoryCommand command)
        {
            var category = _mapper.Map<Category>(command);

            var updateResult = await _catalogContext.Categories
                                        .ReplaceOneAsync(filter: x => x.Id == command.Id, replacement: category);

            var result = updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

            if (result)
            {
                _logger.LogInformation("Kategori : {@category} başarılı bir şekilde güncellendi.", category);
            }
            else
            {
                _logger.LogError("Kategori : {@category}  güncellenirken bir hata oluştu.", category);
            }

            return result;
        }

        public async Task<bool> DeleteCategory(DeleteCategoryCommand command)
        {
            var categoryFilter = Builders<Category>.Filter.Eq(x => x.Id, command.Id);

            var categoryDeleteResult = await _catalogContext.Categories.DeleteOneAsync(categoryFilter);

            var categoryResult = categoryDeleteResult.IsAcknowledged && categoryDeleteResult.DeletedCount > 0;

            if (categoryResult)
            {
                _logger.LogInformation("CategoryId : {@id} li kategori başarılı bir şekilde silindi.", command.Id);

                var productFilter = Builders<Product>.Filter.Eq(x => x.CategoryId, command.Id);

                var productDeleteResult = await _catalogContext.Products.DeleteManyAsync(productFilter);

                var productResult = productDeleteResult.IsAcknowledged && productDeleteResult.DeletedCount > 0;

                if (productResult)
                {
                    _logger.LogInformation("CategoryId : {@id} li ürünler başarılı bir şekilde silindi.", command.Id);
                }
                else
                {
                    _logger.LogError("CategoryId : {@id} li ürünler silinirken bir hata oluştu.", command.Id);
                }
            }
            else
            {
                _logger.LogError("CategoryId : {@id} li kategori silinirken bir hata oluştu.", command.Id);
            }

            return categoryResult;
        }
    }
}
