using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;
using RPSLS.Fullstack.Api.Services.Interfaces;

namespace RPSLS.Fullstack.Application.UseCases;

public sealed class AwardSeriesWinUseCase(IScoreboardService scoreboardService) : IAwardSeriesWinUseCase
{
    public Task ExecuteAsync(AwardSeriesWinRequest request, CancellationToken cancellationToken = default)
    {
        return scoreboardService.RecordSeriesWinAsync(request.PlayerName, cancellationToken);
    }
}
