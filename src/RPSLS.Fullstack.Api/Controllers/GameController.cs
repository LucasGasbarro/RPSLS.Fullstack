using Microsoft.AspNetCore.Mvc;
using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Controllers;

[ApiController]
[Route("play")]
public class GameController(IPlayRoundUseCase playRoundUseCase) : ControllerBase
{
    /// <summary>
    /// Plays a round of RPSSL using the player's selected choice id.
    /// </summary>
    /// <param name="request">The player move payload: { "player": 1..5 }.</param>
    /// <returns>The round result with player and computer choices.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PlayResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlayResponse>> Play([FromBody] PlayRequest request, CancellationToken cancellationToken)
    {
        var response = await playRoundUseCase.ExecuteAsync(request, cancellationToken);
        return Ok(response);
    }
}
