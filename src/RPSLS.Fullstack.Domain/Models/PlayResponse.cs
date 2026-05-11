using System.Text.Json.Serialization;

namespace RPSLS.Fullstack.Api.Models;

/// <summary>
/// Response payload for a played round.
/// </summary>
/// <param name="Results">Outcome text: win, lose, or tie.</param>
/// <param name="Player">Player choice id.</param>
/// <param name="Computer">Computer choice id.</param>
public sealed record PlayResponse(
    [property: JsonPropertyName("results")] string Results,
    [property: JsonPropertyName("player")] int Player,
    [property: JsonPropertyName("computer")] int Computer);
