using Microsoft.EntityFrameworkCore;
using RPSLS.Fullstack.Api.Data;
using RPSLS.Fullstack.Api.Data.Entities;
using RPSLS.Fullstack.Api.Models;
using RPSLS.Fullstack.Api.Services.Interfaces;
using System.Text.RegularExpressions;

namespace RPSLS.Fullstack.Api.Services;

public sealed class ScoreboardService(AppDbContext dbContext) : IScoreboardService
{
    private static readonly Regex PlayerNameTokenRegex = new("[A-Za-z0-9]+", RegexOptions.Compiled);

    public async Task<ScoreEntry> AddAsync(GameRoundResult round, CancellationToken cancellationToken = default)
    {
        var entity = new ScoreEntryEntity
        {
            PlayerChoiceId = round.Player,
            ComputerChoiceId = round.Computer,
            Result = round.Results,
            CreatedAtUtc = DateTime.UtcNow
        };

        dbContext.ScoreEntries.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ScoreEntry(
            entity.Id,
            entity.Result,
            entity.PlayerChoiceId,
            entity.ComputerChoiceId,
            entity.CreatedAtUtc);
    }

    public async Task<IReadOnlyList<ScoreEntry>> GetRecentAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        var entries = await dbContext.ScoreEntries
            .AsNoTracking()
            .OrderByDescending(entry => entry.CreatedAtUtc)
            .ThenByDescending(entry => entry.Id)
            .Take(count)
            .Select(entry => new ScoreEntry(
                entry.Id,
                entry.Result,
                entry.PlayerChoiceId,
                entry.ComputerChoiceId,
                entry.CreatedAtUtc))
            .ToListAsync(cancellationToken);

        return entries;
    }

    public async Task RecordSeriesWinAsync(string playerName, CancellationToken cancellationToken = default)
    {
        var normalizedName = NormalizeToCamelCase(playerName);
        var rankEntry = await dbContext.PlayerRanks.SingleOrDefaultAsync(
            entry => entry.NormalizedName == normalizedName,
            cancellationToken);

        if (rankEntry is null)
        {
            rankEntry = new PlayerRankEntity
            {
                NormalizedName = normalizedName,
                DisplayName = normalizedName,
                Points = 1,
                UpdatedAtUtc = DateTime.UtcNow
            };

            dbContext.PlayerRanks.Add(rankEntry);
        }
        else
        {
            rankEntry.Points += 1;
            rankEntry.DisplayName = normalizedName;
            rankEntry.UpdatedAtUtc = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LeaderboardEntry>> GetLeaderboardAsync(int count = 50, CancellationToken cancellationToken = default)
    {
        var entries = await dbContext.PlayerRanks
            .AsNoTracking()
            .OrderByDescending(entry => entry.Points)
            .ThenByDescending(entry => entry.UpdatedAtUtc)
            .ThenBy(entry => entry.DisplayName)
            .Take(count)
            .Select(entry => new LeaderboardEntry(entry.DisplayName, entry.Points))
            .ToListAsync(cancellationToken);

        return entries;
    }

    public async Task ResetAsync(CancellationToken cancellationToken = default)
    {
        var entries = await dbContext.ScoreEntries.ToListAsync(cancellationToken);
        dbContext.ScoreEntries.RemoveRange(entries);
        var rankEntries = await dbContext.PlayerRanks.ToListAsync(cancellationToken);
        dbContext.PlayerRanks.RemoveRange(rankEntries);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static string NormalizeToCamelCase(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            throw new ArgumentException("Player name is required.", nameof(playerName));
        }

        var tokens = PlayerNameTokenRegex.Matches(playerName.Trim())
            .Select(match => match.Value)
            .Where(value => value.Length > 0)
            .ToArray();

        if (tokens.Length == 0)
        {
            throw new ArgumentException("Player name must contain letters or numbers.", nameof(playerName));
        }

        return string.Concat(tokens.Select(token =>
            token.Length == 1
                ? token.ToUpperInvariant()
                : char.ToUpperInvariant(token[0]) + token[1..].ToLowerInvariant()));
    }
}
