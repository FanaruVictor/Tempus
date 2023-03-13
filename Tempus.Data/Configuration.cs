using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;
using Tempus.Data.Repositories;

namespace Tempus.Data;

public static class Configuration
{
    public static void AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionString:Development"];
        var migrationsAssembly = typeof(TempusDbContext).GetTypeInfo().Assembly.GetName().Name;
        services.AddDbContext<TempusDbContext>(options => options.UseSqlServer(connectionString, sql =>
        {
            sql.MigrationsAssembly(migrationsAssembly);
            sql.MigrationsHistoryTable("__EFMigrationHistory");
        }));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IProfilePhotoRepository, ProfilePhotoRepository>();
    }
}