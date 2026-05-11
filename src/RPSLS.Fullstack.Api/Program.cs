using RPSLS.Fullstack.Api.DependencyInjection;
using RPSLS.Fullstack.Application;
using RPSLS.Fullstack.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiPresentation();

var databasePath = Path.Combine(builder.Environment.ContentRootPath, "App_Data", "rpsls.db");
Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);
builder.Services.AddInfrastructureServices(databasePath);
builder.Services.AddApplicationUseCases();

var app = builder.Build();

await app.ApplyMigrationsAsync();
app.UseApiPipeline();

app.Run();
