using Catalog.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<bool>>
    {
        private readonly IProductCommandRepository _productCommandRepository;
        private readonly IProductQueryRepository _productQueryRepository;

        public UpdateProductCommandHandler(IProductCommandRepository productCommandRepository, IProductQueryRepository productQueryRepository)
        {
            _productCommandRepository = productCommandRepository ?? throw new ArgumentNullException(nameof(productCommandRepository));
            _productQueryRepository = productQueryRepository ?? throw new ArgumentNullException(nameof(productQueryRepository));
        }

        public async Task<ApiResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productQueryRepository.GetProduct(request.Id, false);

            if (product == null)
            {
                return new ApiResponse<bool>();
            }

            var result = await _productCommandRepository.UpdateProduct(request);

            return new ApiResponse<bool> { Data = result, IsSuccess = result };
        }
    }
}
