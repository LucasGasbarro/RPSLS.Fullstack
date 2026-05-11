using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Services.Interfaces;

namespace RPSLS.Fullstack.Application.UseCases;

public sealed class ResetScoreboardUseCase(IScoreboardService scoreboardService) : IResetScoreboardUseCase
{
    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return scoreboardService.ResetAsync(cancellationToken);
    }
}
