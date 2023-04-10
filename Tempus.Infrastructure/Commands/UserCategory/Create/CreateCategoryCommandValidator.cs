﻿using FluentValidation;

namespace Tempus.Infrastructure.Commands.UserCategory.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Color).NotEmpty();
    }
}