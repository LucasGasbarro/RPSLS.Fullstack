using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Services.Interfaces;

public interface IScoreboardService
{
    Task<ScoreEntry> AddAsync(GameRoundResult round, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ScoreEntry>> GetRecentAsync(int count = 10, CancellationToken cancellationToken = default);

    Task RecordSeriesWinAsync(string playerName, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LeaderboardEntry>> GetLeaderboardAsync(int count = 50, CancellationToken cancellationToken = default);

    Task ResetAsync(CancellationToken cancellationToken = default);
}
