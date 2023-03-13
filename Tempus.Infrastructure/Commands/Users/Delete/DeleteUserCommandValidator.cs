using FluentValidation;

namespace Tempus.Infrastructure.Commands.Users.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}