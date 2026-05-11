using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using RPSLS.Fullstack.Api.Models;
using RPSLS.Fullstack.Api.Services.Interfaces;

namespace RPSLS.Fullstack.Api.Services;

public sealed class RandomService(HttpClient httpClient, ILogger<RandomService> logger) : IRandomService
{
    public async Task<Choice> GetRandomChoiceAsync(CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync("/random", cancellationToken);
        response.EnsureSuccessStatusCode();

        var payload = await response.Content.ReadFromJsonAsync<RandomNumberResponse>(cancellationToken: cancellationToken);
        if (payload is null)
        {
            throw new InvalidOperationException("Random number service returned no payload.");
        }

        var choiceId = ((payload.RandomNumber - 1) % ChoiceCatalog.All.Count) + 1;
        if (!ChoiceCatalog.TryGetChoice(choiceId, out var choice))
        {
            logger.LogError("Mapped invalid choice id {ChoiceId} from random number {RandomNumber}.", choiceId, payload.RandomNumber);
            throw new InvalidOperationException("Unable to map random number to choice.");
        }

        return choice;
    }

    private sealed record RandomNumberResponse([property: System.Text.Json.Serialization.JsonPropertyName("random_number")] int RandomNumber);
}
