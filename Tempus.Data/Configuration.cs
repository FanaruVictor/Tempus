using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tempus.Data.Context;

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
    }
}