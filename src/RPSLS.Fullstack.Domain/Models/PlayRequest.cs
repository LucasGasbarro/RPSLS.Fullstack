using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RPSLS.Fullstack.Api.Models;

/// <summary>
/// Request payload for playing a round.
/// </summary>
public sealed class PlayRequest
{
    /// <summary>
    /// Player choice id (1..5).
    /// </summary>
    [JsonPropertyName("player")]
    [Range(1, 5)]
    public int Player { get; init; }
}
