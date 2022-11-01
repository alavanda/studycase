using FluentValidation;
using MongoDB.Bson;

namespace Catalog.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => ObjectId.TryParse(x, out ObjectId objectId))
                .WithMessage("{Id} alanı ObjectId formatına çevirilebilinir olmalıdır.");
        }
    }
}
