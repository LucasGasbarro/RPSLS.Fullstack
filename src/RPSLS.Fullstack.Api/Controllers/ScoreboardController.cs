using Microsoft.AspNetCore.Mvc;
using RPSLS.Fullstack.Api.Application.Interfaces;
using RPSLS.Fullstack.Api.Models;

namespace RPSLS.Fullstack.Api.Controllers;

[ApiController]
[Route("scoreboard")]
public class ScoreboardController(
    IGetScoreboardUseCase getScoreboardUseCase,
    IAwardSeriesWinUseCase awardSeriesWinUseCase,
    IResetScoreboardUseCase resetScoreboardUseCase) : ControllerBase
{
    /// <summary>
    /// Returns the ranking list ordered by points.
    /// </summary>
    /// <returns>Leaderboard entries with normalized player names and points.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LeaderboardEntry>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LeaderboardEntry>>> GetScoreboard(CancellationToken cancellationToken)
    {
        var entries = await getScoreboardUseCase.ExecuteAsync(cancellationToken);
        return Ok(entries);
    }

    /// <summary>
    /// Adds one ranking point for a player that won a best-of-5 series.
    /// </summary>
    /// <param name="request">Winning player name.</param>
    [HttpPost("win")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AwardSeriesWin([FromBody] AwardSeriesWinRequest request, CancellationToken cancellationToken)
    {
        await awardSeriesWinUseCase.ExecuteAsync(request, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Clears leaderboard and historical scoreboard entries.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResetScoreboard(CancellationToken cancellationToken)
    {
        await resetScoreboardUseCase.ExecuteAsync(cancellationToken);
        return NoContent();
    }
}
