using MediatR;

namespace Catalog.Application.Features.Products.Queries
{
    public class GetProductQuery : IRequest<ApiResponse<GetProductQueryResponse>>, IValidateable
    {
        public string Id { get; set; }
    }
}
