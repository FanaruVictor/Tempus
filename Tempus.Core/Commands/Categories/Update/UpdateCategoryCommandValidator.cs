﻿using FluentValidation;

namespace Tempus.Core.Commands.Categories.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Color).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        
    }
}