using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Services.Interfaces;

public interface IGameService
{
    GameRoundResult DetermineRound(int playerChoice, int computerChoice);
}
