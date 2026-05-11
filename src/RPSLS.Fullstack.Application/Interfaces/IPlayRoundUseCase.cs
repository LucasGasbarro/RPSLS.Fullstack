using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Application.Interfaces;

public interface IPlayRoundUseCase
{
    Task<PlayResponse> ExecuteAsync(PlayRequest request, CancellationToken cancellationToken = default);
}
