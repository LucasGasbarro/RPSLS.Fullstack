using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPSLS.Fullstack.Api.Data;
using RPSLS.Fullstack.Api.Services;
using RPSLS.Fullstack.Api.Services.Interfaces;

namespace RPSLS.Fullstack.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string databasePath)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={databasePath}"));

        services.AddSingleton<IGameService, GameService>();
        services.AddScoped<IScoreboardService, ScoreboardService>();
        services.AddHttpClient<IRandomService, RandomService>(client =>
        {
            client.BaseAddress = new Uri("https://codechallenge.boohma.com");
            client.Timeout = TimeSpan.FromSeconds(10);
        });

        return services;
    }
}
