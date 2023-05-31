using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Tempus.Core.Models.Auth;

namespace Tempus.Infrastructure.Services.AuthService;

public class RegisterUserValidation : AbstractValidator<LoginCredentials>
{
    public RegisterUserValidation()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().Must(ValidEmail);
        RuleFor(x => x.ExternalId).NotEmpty();
    }

    private bool ValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}