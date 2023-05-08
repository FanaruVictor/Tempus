using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.SignalR;
using Tempus.Infrastructure.SignalR.Abstractization;

namespace Tempus.API;

public static class ConfigureServices
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services)
    {
        services.AddApiVersioning(o =>
        {
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.ReportApiVersions = true;
            o.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("ver"));
        });
        services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        services.AddScoped<IUserInfo, AspUserInfo>();
        services.AddScoped<IClientEventSender, ClientEventSender>();
        services.AddSingleton<IConnectionManager, ConnectionManager>();
        services.AddSignalR();


        return services;
    }
}