using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Tempus.API;
using Tempus.Data;
using Tempus.Data.Context;
using Tempus.Infrastructure;
using Tempus.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

bool.TryParse(builder.Configuration["isDev"], out bool isDev);
if (!isDev)
{
	builder.Configuration.AddAzureKeyVault(
		$"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/",
		new DefaultKeyVaultSecretManager());
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDb(builder.Configuration);


builder.Services.AddAPIServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.Configure<FormOptions>(o =>
{
	o.ValueLengthLimit = int.MaxValue;
	o.MultipartBodyLengthLimit = int.MaxValue;
	o.MemoryBufferThreshold = int.MaxValue;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey =
				new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

builder.Services.TryAddEnumerable(
	ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>,
		ConfigureJwtBearerOptions>());

builder.Services.AddSwaggerGen(options =>
{
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var allowedCorsHosts = builder.Configuration["AllowedCorsHosts"]?.Split(",");

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policyBuilder =>
		{
			policyBuilder.WithOrigins(allowedCorsHosts ?? new[] { "*" });
			policyBuilder.AllowAnyHeader();
			policyBuilder.AllowAnyMethod();
			policyBuilder.AllowCredentials();
		});
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	using (var scope = app.Services.CreateScope())
	{
		var db = scope.ServiceProvider.GetRequiredService<TempusDbContext>();
		db.Database.EnsureCreated();
	}
}

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
	foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
	{
		options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
			description.GroupName.ToUpperInvariant());
	}
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ClientEventHub>("/hub");

app.Run();

public partial class Program
{
	protected Program()
	{
	}
}