using Microsoft.AspNetCore.Mvc;
using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Controllers;

[ApiController]
[Route("choice")]
public class ChoiceController(IGetRandomChoiceUseCase getRandomChoiceUseCase) : ControllerBase
{
    /// <summary>
    /// Returns a random RPSSL choice.
    /// </summary>
    /// <returns>A single randomly selected choice.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Choice), StatusCodes.Status200OK)]
    public async Task<ActionResult<Choice>> GetChoice(CancellationToken cancellationToken)
    {
        var choice = await getRandomChoiceUseCase.ExecuteAsync(cancellationToken);
        return Ok(choice);
    }
}
