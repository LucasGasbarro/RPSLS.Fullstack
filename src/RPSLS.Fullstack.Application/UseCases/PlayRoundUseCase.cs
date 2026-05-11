using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;
using RPSLS.Fullstack.Api.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace RPSLS.Fullstack.Application.UseCases;

public sealed class PlayRoundUseCase(
    IGameService gameService,
    IRandomService randomService,
    IScoreboardService scoreboardService,
    ILogger<PlayRoundUseCase> logger) : IPlayRoundUseCase
{
    public async Task<PlayResponse> ExecuteAsync(PlayRequest request, CancellationToken cancellationToken = default)
    {
        var computerChoice = await randomService.GetRandomChoiceAsync(cancellationToken);
        var round = gameService.DetermineRound(request.Player, computerChoice.Id);
        await scoreboardService.AddAsync(round, cancellationToken);

        logger.LogInformation("Round played: player={PlayerChoice}, computer={ComputerChoice}, result={Result}", round.Player, round.Computer, round.Results);

        return new PlayResponse(round.Results, round.Player, round.Computer);
    }
}
