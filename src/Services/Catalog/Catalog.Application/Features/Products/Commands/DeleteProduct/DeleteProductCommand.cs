using MediatR;

namespace Catalog.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<ApiResponse<bool>>, IValidateable
    {
        public string Id { get; set; }
    }
}
