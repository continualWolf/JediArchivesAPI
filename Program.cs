using AspNetCoreRateLimit;
using FluentValidation;
using JediArchives.Application.Planets.Validators;
using JediArchives.Application.Users.Commands;
using JediArchives.DataStorage;
using JediArchives.Helper;
using JediArchives.Services.Implementations;
using JediArchives.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5001");

// Add services to the container.
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

// Add validators to pipeline
builder.Services.AddValidatorsFromAssemblyContaining<CreatePlanetCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer(); // for minimal APIs or controller routes
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new() {
        Title = "Jedi Archives API",
        Version = "v1",
        Description = "An API for managing Jedi knowledge across the galaxy."
    });
});

// Code for Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddDbContext<DataContextWrite>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JediArchives_Write")));

builder.Services.AddDbContext<DataContextRead>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JediArchives_Read")));

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Jedi Archives API v1");
        options.DocumentTitle = "Jedi Archives Swagger";
        options.RoutePrefix = "docs"; // Swagger UI at /docs
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext context) => {
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    // If its a validation exception include the attributes that have failed validation
    if (exception is FluentValidation.ValidationException validationException) {
        var errors = validationException.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        var problemDetails = new ValidationProblemDetails(errors) {
            Title = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7807",
            Detail = "One or more fields failed validation.",
            Instance = context.Request.Path
        };

        return Results.ValidationProblem(problemDetails.Errors, statusCode: problemDetails.Status, title: problemDetails.Title, type: problemDetails.Type, detail: problemDetails.Detail, instance: problemDetails.Instance);
    }

    // For most exceptions that are thrown just return a 500 as a general error
    return Results.Problem(
        statusCode: 500,
        title: "An unexpected error occurred.",
        detail: exception?.Message,
        instance: context.Request.Path
    );
});

app.Run();

public partial class Program { }