using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Tempus.Core.Models.Auth;

namespace Tempus.Infrastructure.Services.AuthService;

public class RegisterUserValidation : AbstractValidator<RegistrationData>
{
    public RegisterUserValidation()
    {
        RuleFor(x => x.Email).NotEmpty().Must(ValidEmail);
    }

    private bool ValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}