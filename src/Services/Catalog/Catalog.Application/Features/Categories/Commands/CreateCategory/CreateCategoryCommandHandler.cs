using Catalog.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<string>>
    {
        private readonly ICategoryCommandRepository _categoryCommandRepository;

        public CreateCategoryCommandHandler(ICategoryCommandRepository categoryCommandRepository)
        {
            _categoryCommandRepository = categoryCommandRepository ?? throw new ArgumentNullException(nameof(categoryCommandRepository));
        }

        public async Task<ApiResponse<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryCommandRepository.CreateCategory(request);

            return new ApiResponse<string> { Data = result };
        }
    }
}