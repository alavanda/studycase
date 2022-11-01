using FluentValidation;

namespace Catalog.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{Name} alanı zorunludur.")
                .NotNull().WithMessage("{Name} alanı zorunludur.");
        }
    }
}