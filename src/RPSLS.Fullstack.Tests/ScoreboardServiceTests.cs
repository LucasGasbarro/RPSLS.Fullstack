using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RPSLS.Fullstack.Api.Data;
using RPSLS.Fullstack.Api.Models;
using RPSLS.Fullstack.Api.Services;

namespace RPSLS.Fullstack.Tests;

public sealed class ScoreboardServiceTests
{
    [Fact]
    public async Task AddAsync_And_GetRecentAsync_ReturnsMostRecentEntries()
    {
        // What: Verify scoreboard stores rounds and returns only the latest 10 entries.
        // Why: The leaderboard/history view depends on stable "most recent first" behavior.
        // How: Insert 11 rounds, fetch recent entries, and assert count/order boundaries.
        await using var fixture = await SqliteFixture.CreateAsync();
        var service = new ScoreboardService(fixture.Context);

        for (var i = 1; i <= 11; i++)
        {
            var result = i % 2 == 0 ? "win" : "lose";
            var playerChoice = (i % 5) + 1;
            var computerChoice = ((i + 1) % 5) + 1;
            var round = new GameRoundResult(result, playerChoice, computerChoice);

            await service.AddAsync(round);
        }

        var recent = await service.GetRecentAsync();

        Assert.Equal(10, recent.Count);
        Assert.Equal(11, recent[0].Id);
        Assert.Equal(2, recent[^1].Id);
    }

    [Fact]
    public async Task ResetAsync_RemovesAllEntries()
    {
        // What: Verify reset clears persisted scoreboard data.
        // Why: Reset action in the UI must leave no stale history behind.
        // How: Insert data, call reset, fetch recent entries, assert empty result.
        await using var fixture = await SqliteFixture.CreateAsync();
        var service = new ScoreboardService(fixture.Context);

        await service.AddAsync(new GameRoundResult("win", 1, 2));
        await service.AddAsync(new GameRoundResult("lose", 3, 1));

        await service.ResetAsync();

        var recent = await service.GetRecentAsync();

        Assert.Empty(recent);
    }

    [Fact]
    public async Task RecordSeriesWinAsync_NormalizesPlayerNameToCamelCaseAndAggregatesPoints()
    {
        // What: Verify series-win ranking normalizes names to CamelCase and aggregates points.
        // Why: Same user typed in different formats should map to one ranking identity.
        // How: Record wins with two name variants and assert one merged leaderboard row.
        await using var fixture = await SqliteFixture.CreateAsync();
        var service = new ScoreboardService(fixture.Context);

        await service.RecordSeriesWinAsync("joHN doE");
        await service.RecordSeriesWinAsync("john_doe");

        var leaderboard = await service.GetLeaderboardAsync();

        Assert.Single(leaderboard);
        Assert.Equal("JohnDoe", leaderboard[0].PlayerName);
        Assert.Equal(2, leaderboard[0].Points);
    }

    private sealed class SqliteFixture : IAsyncDisposable
    {
        private readonly SqliteConnection _connection;

        public AppDbContext Context { get; }

        private SqliteFixture(SqliteConnection connection, AppDbContext context)
        {
            _connection = connection;
            Context = context;
        }

        public static async Task<SqliteFixture> CreateAsync()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            await connection.OpenAsync();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();

            return new SqliteFixture(connection, context);
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
            await _connection.DisposeAsync();
        }
    }
}
