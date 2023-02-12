using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

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

        return services;
    }
}