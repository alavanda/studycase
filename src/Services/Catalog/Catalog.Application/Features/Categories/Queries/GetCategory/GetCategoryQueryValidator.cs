using FluentValidation;
using MongoDB.Bson;

namespace Catalog.Application.Features.Categories.Queries
{
    public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public GetCategoryQueryValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => ObjectId.TryParse(x, out ObjectId objectId))
                .WithMessage("{Id} alanı ObjectId formatına çevirilebilinir olmalıdır.");
        }
    }
}
