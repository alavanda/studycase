using Catalog.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ApiResponse<GetProductQueryResponse>>
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public GetProductQueryHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository ?? throw new ArgumentNullException(nameof(productQueryRepository));
        }

        public async Task<ApiResponse<GetProductQueryResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _productQueryRepository.GetProduct(request.Id);

            return new ApiResponse<GetProductQueryResponse> { Data = result };
        }
    }
}
