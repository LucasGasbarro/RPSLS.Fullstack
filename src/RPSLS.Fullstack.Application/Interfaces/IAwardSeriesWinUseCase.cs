using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Application.Interfaces;

public interface IAwardSeriesWinUseCase
{
    Task ExecuteAsync(AwardSeriesWinRequest request, CancellationToken cancellationToken = default);
}
