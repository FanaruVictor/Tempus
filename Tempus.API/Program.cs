using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Tempus.Core.Commands.Users.Create;
using Tempus.Core.Queries.Users.GetAll;
using Tempus.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDb(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddMediatR(typeof(GetAllUsersQuery).Assembly);

var allowedCorsHosts = builder.Configuration["AllowedCorsHosts"];

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            policyBuilder =>
            {
                policyBuilder.WithOrigins(allowedCorsHosts ?? "*");
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
            });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    protected Program()
    {
    }
}