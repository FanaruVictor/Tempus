using FluentValidation;

namespace Tempus.Infrastructure.Commands.Categories.Create;

public class CreateCategoryCommandValidator :  AbstractValidator<CreateCategoryCommand> 
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Color).NotEmpty();
    }
}