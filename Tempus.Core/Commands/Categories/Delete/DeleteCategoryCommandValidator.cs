using FluentValidation;

namespace Tempus.Core.Commands.Categories.Delete;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        
    }
}