namespace RPSLS.Fullstack.Api.Models;

/// <summary>
/// Represents one RPSSL move option.
/// </summary>
/// <param name="Id">Numeric id of the choice.</param>
/// <param name="Name">Display name of the choice.</param>
public sealed record Choice(int Id, string Name);

/// <summary>
/// Provides access to the fixed RPSSL choice set.
/// </summary>
public static class ChoiceCatalog
{
    private static readonly Choice[] Choices =
    [
        new(1, "rock"),
        new(2, "paper"),
        new(3, "scissors"),
        new(4, "lizard"),
        new(5, "spock"),
    ];

    /// <summary>
    /// Gets all available RPSSL choices.
    /// </summary>
    public static IReadOnlyList<Choice> All => Choices;

    /// <summary>
    /// Tries to retrieve a choice by id.
    /// </summary>
    /// <param name="id">Choice id.</param>
    /// <param name="choice">Matched choice when found.</param>
    /// <returns><c>true</c> when the id exists; otherwise <c>false</c>.</returns>
    public static bool TryGetChoice(int id, out Choice choice)
    {
        choice = Choices.FirstOrDefault(c => c.Id == id)!;
        return choice is not null;
    }
}
