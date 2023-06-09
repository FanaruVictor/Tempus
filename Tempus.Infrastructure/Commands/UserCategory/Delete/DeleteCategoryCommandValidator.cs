﻿using FluentValidation;

namespace Tempus.Infrastructure.Commands.UserCategory.Delete;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}