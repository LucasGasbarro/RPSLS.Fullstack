using Microsoft.AspNetCore.Mvc;
using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Controllers;

[ApiController]
[Route("choices")]
public class ChoicesController(IGetChoicesUseCase getChoicesUseCase) : ControllerBase
{
    /// <summary>
    /// Returns the complete list of RPSSL choices.
    /// </summary>
    /// <returns>All available choices with id and name.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<Choice>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Choice>>> GetChoices(CancellationToken cancellationToken)
    {
        var choices = await getChoicesUseCase.ExecuteAsync(cancellationToken);
        return Ok(choices);
    }
}
