﻿using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Registrations;

namespace Tempus.Infrastructure.Commands.Registrations.Update;

public class UpdateRegistrationCommand : BaseRequest<BaseResponse<RegistrationDetails>>
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
}