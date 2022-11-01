using Catalog.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse<bool>>
    {
        private readonly ICategoryCommandRepository _categoryCommandRepository;
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public UpdateCategoryCommandHandler(ICategoryCommandRepository categoryCommandRepository, ICategoryQueryRepository categoryQueryRepository)
        {
            _categoryCommandRepository = categoryCommandRepository ?? throw new ArgumentNullException(nameof(categoryCommandRepository));
            _categoryQueryRepository = categoryQueryRepository ?? throw new ArgumentNullException(nameof(categoryQueryRepository));
        }

        public async Task<ApiResponse<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryQueryRepository.GetCategory(request.Id);

            if (category == null)
            {
                return new ApiResponse<bool>();
            }

            var result = await _categoryCommandRepository.UpdateCategory(request);

            return new ApiResponse<bool> { Data = result, IsSuccess = result };
        }
    }
}
