using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Services.Interfaces;

public interface IRandomService
{
    Task<Choice> GetRandomChoiceAsync(CancellationToken cancellationToken = default);
}
