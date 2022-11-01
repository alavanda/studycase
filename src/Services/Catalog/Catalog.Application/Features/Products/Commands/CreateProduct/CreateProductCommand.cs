using MediatR;

namespace Catalog.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<ApiResponse<string>>, IValidateable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
