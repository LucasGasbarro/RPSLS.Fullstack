using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Application.Interfaces;

public interface IGetScoreboardUseCase
{
    Task<IReadOnlyList<LeaderboardEntry>> ExecuteAsync(CancellationToken cancellationToken = default);
}
