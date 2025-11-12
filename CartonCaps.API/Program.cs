namespace CartonCaps.API;

using CartonCaps.Application.Handlers;
using CartonCaps.Domain.Repositories;
using CartonCaps.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using System;
using Microsoft.AspNetCore.RateLimiting;

/// <summary>
/// the apps entrypoint.
/// </summary>
public static class Program
{
    /// <summary>
    /// the main method.
    /// </summary>
    /// <param name="args">Come from the cmd.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;
        var securityKey = configuration["Jwt:SecurityKey"]
                          ?? throw new InvalidOperationException("Jwt:SecurityKey not configured.");
        var issuer = configuration["Jwt:Issuer"]
                     ?? throw new InvalidOperationException("Jwt:Issuer not configured.");
        var audience = configuration["Jwt:Audience"]
                       ?? throw new InvalidOperationException("Jwt:Audience not configured.");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    #pragma warning disable CA5404 // Do not disable token validation checks
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    #pragma warning restore CA5404 // Do not disable token validation checks
                    ValidateIssuerSigningKey = false,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),

                    NameClaimType = ClaimTypes.NameIdentifier,
                };
            });
        var permitLimit = configuration.GetValue("RateLimit:PermitLimit", 5);
        var windowSeconds = configuration.GetValue("RateLimit:WindowSeconds", 30);
        var queueLimit = configuration.GetValue("RateLimit:QueueLimit", 2);

        builder.Services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter(policyName: "fixed-create-limit", fixedWindowOptions =>
            {
                fixedWindowOptions.PermitLimit = permitLimit;
                fixedWindowOptions.Window = TimeSpan.FromSeconds(windowSeconds);
                fixedWindowOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                fixedWindowOptions.QueueLimit = queueLimit;
            });

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateReferralCommandHandler).Assembly));
        builder.Services.AddSingleton<IReferralRepository, InMemoryReferralRepository>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGet("/ping", () => "pong");

        app.UseHttpsRedirection();
        app.UseAuthentication();

        app.Use(async (context, next) =>
        {
            if (context.User.Identity?.IsAuthenticated == true && !context.Items.ContainsKey("UserId"))
            {
                var userIdString = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out Guid userIdGuid))
                {
                    context.Items["UserId"] = userIdGuid;
                }
            }

            await next().ConfigureAwait(false);
        });

        app.UseRateLimiter();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}