namespace CartonCaps.API;

using CartonCaps.Application.Handlers;
using CartonCaps.Domain.Repositories;
using CartonCaps.Infrastructure.Persistence;

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
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}