using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Application.Interfaces;

public interface IGetRandomChoiceUseCase
{
    Task<Choice> ExecuteAsync(CancellationToken cancellationToken = default);
}
