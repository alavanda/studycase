using Catalog.Application.Features.Categories.Queries;
using System.Threading.Tasks;

namespace Catalog.Application.Contracts.Persistence
{
    public interface ICategoryQueryRepository
    {
        Task<GetCategoryQueryResponse> GetCategory(string id);
    }
}
