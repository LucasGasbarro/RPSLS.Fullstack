using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;
using RPSLS.Fullstack.Api.Services.Interfaces;

namespace RPSLS.Fullstack.Application.UseCases;

public sealed class GetScoreboardUseCase(IScoreboardService scoreboardService) : IGetScoreboardUseCase
{
    public Task<IReadOnlyList<LeaderboardEntry>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return scoreboardService.GetLeaderboardAsync(cancellationToken: cancellationToken);
    }
}
