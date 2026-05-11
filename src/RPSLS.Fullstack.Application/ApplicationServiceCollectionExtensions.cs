using Microsoft.Extensions.DependencyInjection;
using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Application.UseCases;

namespace RPSLS.Fullstack.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetChoicesUseCase, GetChoicesUseCase>();
        services.AddScoped<IGetRandomChoiceUseCase, GetRandomChoiceUseCase>();
        services.AddScoped<IPlayRoundUseCase, PlayRoundUseCase>();
        services.AddScoped<IGetScoreboardUseCase, GetScoreboardUseCase>();
        services.AddScoped<IAwardSeriesWinUseCase, AwardSeriesWinUseCase>();
        services.AddScoped<IResetScoreboardUseCase, ResetScoreboardUseCase>();

        return services;
    }
}
