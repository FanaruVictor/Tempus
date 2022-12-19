using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Tempus.Core.Commands.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().Must(ValidEmail);
    }

    private bool ValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}