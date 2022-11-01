using Catalog.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Categories.Queries
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ApiResponse<GetCategoryQueryResponse>>
    {
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public GetCategoryQueryHandler(ICategoryQueryRepository categoryQueryRepository)
        {
            _categoryQueryRepository = categoryQueryRepository ?? throw new ArgumentNullException(nameof(categoryQueryRepository));
        }

        public async Task<ApiResponse<GetCategoryQueryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryQueryRepository.GetCategory(request.Id);

            return new ApiResponse<GetCategoryQueryResponse> { Data = result };
        }
    }
}
