using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tempus.Data.Context;
using static Tempus.IntegrationTests.Configuration.DbSeed;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Tempus.IntegrationTests.Configuration;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public string DefaultUserId { get; set; } = "6627df4f-6ac6-4ff6-bf8e-6d358fd88025";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<TempusDbContext>));

            if(descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.Configure<TestAuthHandlerOptions>(options => options.DefaultUserId = DefaultUserId);

            services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme,
                    options => { });

            services.AddDbContext<TempusDbContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TempusDbContext>();
            if(context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                context.Database.Migrate();
            }

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            SeedUsers(context);
            SeedCategories(context);
            SeedRegistration(context);
        });

        builder.UseEnvironment("Development");
    }
}