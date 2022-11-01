using Catalog.Application.Features.Products.Queries;
using System.Threading.Tasks;

namespace Catalog.Application.Contracts.Persistence
{
    public interface IProductQueryRepository
    {
        Task<GetProductQueryResponse> GetProduct(string id, bool fromCache = true);
    }
}
