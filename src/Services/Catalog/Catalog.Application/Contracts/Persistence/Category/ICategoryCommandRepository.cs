using Catalog.Application.Features.Categories.Commands;
using System.Threading.Tasks;

namespace Catalog.Application.Contracts.Persistence
{
    public interface ICategoryCommandRepository
    {
        Task<string> CreateCategory(CreateCategoryCommand command);
        Task<bool> UpdateCategory(UpdateCategoryCommand command);
        Task<bool> DeleteCategory(DeleteCategoryCommand command);
    }
}