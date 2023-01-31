using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tempus.Infrastructure.Commands.Auth.Register;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Queries.Users.GetAll;

namespace Tempus.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatrRequestContextBehaviour<,>));
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddMediatR(typeof(GetAllUsersQuery).Assembly);
        return services;
    }
}