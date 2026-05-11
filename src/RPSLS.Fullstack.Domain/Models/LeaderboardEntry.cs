using System.Text.Json.Serialization;

namespace RPSLS.Fullstack.Api.Models;

/// <summary>
/// One leaderboard row with normalized player name and accumulated points.
/// </summary>
/// <param name="PlayerName">CamelCase normalized player name.</param>
/// <param name="Points">Total points (series wins).</param>
public sealed record LeaderboardEntry(
    [property: JsonPropertyName("playerName")] string PlayerName,
    [property: JsonPropertyName("points")] int Points);
