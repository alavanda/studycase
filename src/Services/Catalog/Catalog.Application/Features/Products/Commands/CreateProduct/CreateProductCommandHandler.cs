using Catalog.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<string>>
    {
        private readonly IProductCommandRepository _productCommandRepository;

        public CreateProductCommandHandler(IProductCommandRepository productCommandRepository)
        {
            _productCommandRepository = productCommandRepository ?? throw new ArgumentNullException(nameof(productCommandRepository));
        }

        public async Task<ApiResponse<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productCommandRepository.CreateProduct(request);

            return new ApiResponse<string> { Data = result };
        }
    }
}
