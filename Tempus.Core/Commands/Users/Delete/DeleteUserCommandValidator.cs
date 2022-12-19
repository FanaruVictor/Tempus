using FluentValidation;

namespace Tempus.Core.Commands.Users.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}