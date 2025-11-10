var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/ping", () => "pong")
    .WithName("Ping")
    .WithOpenApi();

app.Run();
