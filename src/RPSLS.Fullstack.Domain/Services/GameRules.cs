namespace RPSLS.Fullstack.Api.Services;

internal static class GameRules
{
    private static readonly IReadOnlyDictionary<int, int[]> Wins = new Dictionary<int, int[]>
    {
        [1] = [3, 4],
        [2] = [1, 5],
        [3] = [2, 4],
        [4] = [2, 5],
        [5] = [1, 3],
    };

    public static string DetermineResult(int playerChoice, int computerChoice)
    {
        if (playerChoice == computerChoice)
        {
            return "tie";
        }

        return Wins[playerChoice].Contains(computerChoice) ? "win" : "lose";
    }
}
