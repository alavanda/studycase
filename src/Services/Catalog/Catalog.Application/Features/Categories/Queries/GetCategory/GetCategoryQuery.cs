using MediatR;

namespace Catalog.Application.Features.Categories.Queries
{
    public class GetCategoryQuery : IRequest<ApiResponse<GetCategoryQueryResponse>>, IValidateable
    {
        public string Id { get; set; }
    }
}
