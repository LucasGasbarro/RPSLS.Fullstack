using RPSLS.Fullstack.Api.Services;

namespace RPSLS.Fullstack.Tests;

public sealed class GameServiceTests
{
    private readonly GameService _service = new();

    public static IEnumerable<object[]> Rounds =>
        from player in new[] { 1, 2, 3, 4, 5 }
        from computer in new[] { 1, 2, 3, 4, 5 }
        select new object[] { player, computer, ExpectedResult(player, computer) };

    [Theory]
    [MemberData(nameof(Rounds))]
    public void DetermineRound_ReturnsExpectedResult(int player, int computer, string expected)
    {
        // What: Validate RPSSL rules for every possible player/computer pair (25 combinations).
        // Why: This is the core game logic; any wrong mapping breaks round outcomes.
        // How: Use table-driven test data and compare returned result/choices with expected values.
        var round = _service.DetermineRound(player, computer);

        Assert.Equal(expected, round.Results);
        Assert.Equal(player, round.Player);
        Assert.Equal(computer, round.Computer);
    }

    private static string ExpectedResult(int player, int computer) => player switch
    {
        _ when player == computer => "tie",
        1 when computer is 3 or 4 => "win",
        2 when computer is 1 or 5 => "win",
        3 when computer is 2 or 4 => "win",
        4 when computer is 2 or 5 => "win",
        5 when computer is 1 or 3 => "win",
        _ => "lose"
    };
}
