using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RPSLS.Fullstack.Api.Models;

/// <summary>
/// Request payload to record one best-of-5 series win for a player.
/// </summary>
public sealed class AwardSeriesWinRequest
{
    /// <summary>
    /// Player name that will receive one ranking point.
    /// </summary>
    [JsonPropertyName("playerName")]
    [Required]
    [MinLength(1)]
    public string PlayerName { get; init; } = string.Empty;
}
