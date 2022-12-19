﻿using FluentValidation;

namespace Tempus.Core.Commands.Registrations.Delete;

public class DeleteRegistrationCommandValidator : AbstractValidator<DeleteRegistrationCommand>
{
    public DeleteRegistrationCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}