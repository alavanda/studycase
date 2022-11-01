using FluentValidation;
using MongoDB.Bson;

namespace Catalog.Application.Features.Products.Commands
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .Must(x => ObjectId.TryParse(x, out ObjectId objectId))
                .WithMessage("{CategoryId} alanı ObjectId formatına çevirilebilinir olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{Name} alanı zorunludur.")
                .NotNull().WithMessage("{Name} alanı zorunludur.");

            RuleFor(x => x.Price)
                .GreaterThan(0.00M).WithMessage("{Price} alanı 0.00'dan büyük olmalıdır.")
                .ScalePrecision(2, 10).WithMessage("{Price} alanı uygun formatta değil.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("{Currency} alanı zorunludur.")
                .NotNull().WithMessage("{Currency} alanı zorunludur.");
        }
    }
}
