using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Tempus.Infrastructure.Commands.Users.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().Must(ValidEmail);
    }

    private bool ValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}