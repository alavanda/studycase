using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Features.Products.Commands;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductCommandRepository : IProductCommandRepository
    {
        private readonly ICatalogContext _catalogContext;
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCommandRepository> _logger;

        public ProductCommandRepository(
            ICatalogContext catalogContext,
            ICategoryQueryRepository categoryQueryRepository,
            IMapper mapper,
            ILogger<ProductCommandRepository> logger
            )
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
            _categoryQueryRepository = categoryQueryRepository ?? throw new ArgumentNullException(nameof(categoryQueryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> CreateProduct(CreateProductCommand command)
        {
            var category = await _categoryQueryRepository.GetCategory(command.CategoryId);

            if (category == null)
            {
                _logger.LogInformation("Böyle bir kategori yok");

                return null;
            }

            var product = _mapper.Map<Product>(command);

            await _catalogContext.Products.InsertOneAsync(product);

            if (!string.IsNullOrEmpty(product.Id))
            {
                _logger.LogInformation("Ürün : {@product} başarılı bir şekilde oluşturuldu.", product);
            }

            return product.Id;
        }

        public async Task<bool> UpdateProduct(UpdateProductCommand command)
        {
            var category = await _categoryQueryRepository.GetCategory(command.CategoryId);

            if (category == null)
            {
                return false;
            }

            var product = _mapper.Map<Product>(command);

            var updateResult = await _catalogContext.Products
                                        .ReplaceOneAsync(filter: x => x.Id == command.Id, replacement: product);

            var result = updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

            if (result)
            {
                _logger.LogInformation("Ürün : {@product} başarılı bir şekilde güncellendi.", product);
            }
            else
            {
                _logger.LogError("Ürün : {@product} güncellenirken bir hata oluştu.", product);
            }

            return result;
        }

        public async Task<bool> DeleteProduct(DeleteProductCommand command)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, command.Id);

            var deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

            var result = deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

            if (result)
            {
                _logger.LogInformation("ProductId => {@id} li ürün başarılı bir şekilde silindi.", command.Id);
            }
            else
            {
                _logger.LogError("ProductId => {@id} li ürün silinirken bir hata oluştu.", command.Id);
            }

            return result;
        }
    }
}
