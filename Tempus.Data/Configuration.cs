using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;
using Tempus.Data.Repositories;
using Tempus.Data.Repositories.Group;

namespace Tempus.Data;

public static class Configuration
{
	public static void AddDb(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration["tempus-connection-string"];
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
		services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();
		services.AddScoped<IGroupRepository, GroupRepository>();
		services.AddScoped<IGroupUserRepository, GroupUserRepository>();
		services.AddScoped<IGroupPhotoRepository, GroupPhotoRepository>();
		services.AddScoped<IGroupCategoryRepository, GroupCategoryRepository>();
		services.AddScoped<IUserCategoryRepository, UserCategoryRepository>();
	}
}