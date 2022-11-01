using FluentValidation;
using MongoDB.Bson;

namespace Catalog.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => ObjectId.TryParse(x, out ObjectId objectId))
                .WithMessage("{Id} alanı ObjectId formatına çevirilebilinir olmalıdır.");
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{Name} alanı zorunludur.")
                .NotNull().WithMessage("{Name} alanı zorunludur.");
        }
    }
}
