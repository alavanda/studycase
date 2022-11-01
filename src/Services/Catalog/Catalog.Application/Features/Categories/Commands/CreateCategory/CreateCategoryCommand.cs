using MediatR;

namespace Catalog.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<ApiResponse<string>>, IValidateable
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
