using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Application.UseCases;

public sealed class GetChoicesUseCase : IGetChoicesUseCase
{
    public Task<IReadOnlyList<Choice>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult((IReadOnlyList<Choice>)ChoiceCatalog.All);
    }
}
