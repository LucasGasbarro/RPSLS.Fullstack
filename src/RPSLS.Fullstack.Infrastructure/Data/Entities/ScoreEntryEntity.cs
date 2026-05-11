namespace RPSLS.Fullstack.Api.Data.Entities;

public sealed class ScoreEntryEntity
{
    public int Id { get; set; }

    public int PlayerChoiceId { get; set; }

    public int ComputerChoiceId { get; set; }

    public string Result { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}
