using FluentValidation;
using MongoDB.Bson;

namespace Catalog.Application.Features.Products.Commands
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => ObjectId.TryParse(x, out ObjectId objectId))
                .WithMessage("{Id} alanı ObjectId formatına çevirilebilinir olmalıdır.");
        }
    }
}
