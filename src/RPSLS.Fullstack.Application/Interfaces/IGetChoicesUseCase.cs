using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Application.Interfaces;

public interface IGetChoicesUseCase
{
    Task<IReadOnlyList<Choice>> ExecuteAsync(CancellationToken cancellationToken = default);
}
