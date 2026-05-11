namespace RPSLS.Fullstack.Api.Application.Interfaces;

public interface IResetScoreboardUseCase
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
