using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Tempus.Infrastructure.Models.Auth;

namespace Tempus.Infrastructure.Services.AuthService;

public class RegisterUserValidation : AbstractValidator<RegistrationData>
{
    public RegisterUserValidation()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().Must(ValidEmail);
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }

    private bool ValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}