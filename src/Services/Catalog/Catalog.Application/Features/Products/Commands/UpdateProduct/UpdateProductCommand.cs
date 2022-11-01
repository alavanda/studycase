using MediatR;

namespace Catalog.Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<ApiResponse<bool>>, IValidateable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
