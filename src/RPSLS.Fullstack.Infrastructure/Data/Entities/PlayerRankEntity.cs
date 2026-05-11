namespace RPSLS.Fullstack.Api.Data.Entities;

public sealed class PlayerRankEntity
{
    public string NormalizedName { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public int Points { get; set; }

    public DateTime UpdatedAtUtc { get; set; }
}
