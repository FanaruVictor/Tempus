using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tempus.Infrastructure.Commands.Auth.Register;
using Tempus.Infrastructure.Queries.Users.GetAll;

namespace Tempus.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
        services.AddMediatR(typeof(GetAllUsersQuery).Assembly);

        return services;
    }
}