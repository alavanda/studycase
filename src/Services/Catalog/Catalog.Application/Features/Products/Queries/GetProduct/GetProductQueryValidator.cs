using FluentValidation;
using MongoDB.Bson;

namespace Catalog.Application.Features.Products.Queries
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => ObjectId.TryParse(x, out ObjectId objectId))
                .WithMessage("{Id} alanı ObjectId formatına çevirilebilinir olmalıdır.");
        }
    }
}
