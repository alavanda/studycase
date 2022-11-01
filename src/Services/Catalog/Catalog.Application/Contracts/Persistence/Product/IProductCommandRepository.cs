using Catalog.Application.Features.Products.Commands;
using System.Threading.Tasks;

namespace Catalog.Application.Contracts.Persistence
{
    public interface IProductCommandRepository
    {
        Task<string> CreateProduct(CreateProductCommand command);
        Task<bool> UpdateProduct(UpdateProductCommand command);
        Task<bool> DeleteProduct(DeleteProductCommand command);
    }
}
