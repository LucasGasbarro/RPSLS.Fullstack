using System.Text.Json.Serialization;

namespace RPSLS.Fullstack.Api.Models;

/// <summary>
/// Represents one persisted round in the scoreboard.
/// </summary>
/// <param name="Id">Score entry id.</param>
/// <param name="Results">Outcome text: win, lose, or tie.</param>
/// <param name="Player">Player choice id.</param>
/// <param name="Computer">Computer choice id.</param>
/// <param name="CreatedAtUtc">Creation timestamp in UTC.</param>
public sealed record ScoreEntry(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("results")] string Results,
    [property: JsonPropertyName("player")] int Player,
    [property: JsonPropertyName("computer")] int Computer,
    [property: JsonPropertyName("createdAtUtc")] DateTime CreatedAtUtc);
