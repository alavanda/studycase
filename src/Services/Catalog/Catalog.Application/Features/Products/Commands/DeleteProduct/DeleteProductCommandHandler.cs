using Catalog.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse<bool>>
    {
        private readonly IProductCommandRepository _productCommandRepository;

        public DeleteProductCommandHandler(IProductCommandRepository productCommandRepository)
        {
            _productCommandRepository = productCommandRepository ?? throw new ArgumentNullException(nameof(productCommandRepository));
        }

        public async Task<ApiResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productCommandRepository.DeleteProduct(request);

            return new ApiResponse<bool> { Data = result, IsSuccess = result };
        }
    }
}
