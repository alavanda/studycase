using MediatR;

namespace Catalog.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<ApiResponse<bool>>, IValidateable
    {
        public string Id { get; set; }
    }
}
