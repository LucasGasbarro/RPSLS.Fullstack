using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;
using RPSLS.Fullstack.Api.Services.Interfaces;

namespace RPSLS.Fullstack.Application.UseCases;

public sealed class GetRandomChoiceUseCase(IRandomService randomService) : IGetRandomChoiceUseCase
{
    public Task<Choice> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return randomService.GetRandomChoiceAsync(cancellationToken);
    }
}
