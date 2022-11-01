using MediatR;

namespace Catalog.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<ApiResponse<bool>>, IValidateable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
