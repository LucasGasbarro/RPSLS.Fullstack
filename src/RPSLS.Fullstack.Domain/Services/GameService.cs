using RPSLS.Fullstack.Api.Models;
using RPSLS.Fullstack.Api.Services.Interfaces;

namespace RPSLS.Fullstack.Api.Services;

public sealed class GameService : IGameService
{
    public GameRoundResult DetermineRound(int playerChoice, int computerChoice)
    {
        var results = GameRules.DetermineResult(playerChoice, computerChoice);
        return new GameRoundResult(results, playerChoice, computerChoice);
    }
}
