using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tempus.Core.IServices;
using Tempus.Infrastructure.Commands.UserCategory.Create;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Queries.Users.GetAll;
using Tempus.Infrastructure.Services.AuthService;
using Tempus.Infrastructure.Services.Cloudynary;
using Tempus.Infrastructure.SignalR;
using Tempus.Infrastructure.SignalR.Abstractization;

namespace Tempus.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatrRequestContextBehaviour<,>));
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddSingleton<IConnectionManager, ConnectionManager>();
        services.AddMediatR(typeof(GetAllUsersQuery).Assembly);
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}