namespace CartonCaps.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using CartonCaps.Application.Commands;
using CartonCaps.Application.Queries;
using CartonCaps.Domain.Exceptions;

/// <summary>
/// Small DTO wrapping the email string just because it is not possible to simply pass a string.
/// </summary>
/// <param name="Email">email string.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record MarkRegisteredEmailRequest([System.ComponentModel.DataAnnotations.Required] string Email);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter

/// <summary>
/// Presentation layer exposing the referral usecases defined in application layer.
/// </summary>
/// <param name="mediator">Injected by the DI.</param>
[ApiController]
[Route("api/referrals")]
public class ReferralsController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator mediator = mediator;

    /// <summary>
    /// Create new referral entry.
    /// </summary>
    /// <param name="command">contains the referral code and the tracking id.</param>
    /// <returns>The timestamp of when it was created or the reason why failed.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReferral(CreateReferralCommand command)
    {
        try
        {
            Guid referralId = await this.mediator.Send(command).ConfigureAwait(true);
            return this.CreatedAtAction(
                nameof(this.GetReferralById),
                new { referralId },
                referralId);
        }
        catch (DomainException ex)
        {
            return this.BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Returns a single detailed referral.
    /// </summary>
    /// <param name="referralId">The referral identifier.</param>
    /// <returns>the result object or the detailed error message.</returns>
    [HttpGet("{referralId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReferralDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReferralById([FromRoute] string referralId)
    {
        try
        {
            var query = new GetReferralByReferralIdQuery(referralId);
            ReferralDto? result = await this.mediator.Send(query).ConfigureAwait(true);
            return result is null ? this.NotFound() : this.Ok(result);
        }
        catch (Exception e) when (e is ArgumentNullException || e is ArgumentException)
        {
            return this.BadRequest(new { error = e.Message });
        }
    }

    /// <summary>
    /// Mark the referrel as installed,
    /// this is to be called by the third party that handles the deep link.
    /// </summary>
    /// <param name="command">tracking id.</param>
    /// <returns>The result of the operation, whether it was successful or not.</returns>
    [HttpPut("installed")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkInstalled([FromBody] MarkInstalledCommand command)
    {
        try
        {
            await this.mediator.Send(command).ConfigureAwait(true);
            return this.NoContent();
        }
        catch (ArgumentException)
        {
            return this.NotFound();
        }
        catch (DomainException ex)
        {
            return this.BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Updates the status to registered and tracks the email of the person.
    /// </summary>
    /// <param name="referralId">The identifier of the referral to update.</param>
    /// <param name="request">The email of the user registered.</param>
    /// <returns>The result of the operation, detailed error codes and description.</returns>
    [HttpPut("{referralId:guid}/registered")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetReferralStatusToRegistered([FromRoute] string referralId, [FromBody] MarkRegisteredEmailRequest request)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            await this.mediator.Send(new MarkRegisteredCommand(referralId, request.Email)).ConfigureAwait(true);
            return this.NoContent();
        }
        catch (ArgumentNullException e)
        {
            return this.BadRequest(new { error = e.Message });
        }
        catch (ArgumentException e)
        {
            return this.NotFound(new { error = e.Message });
        }
    }

    /// <summary>
    /// Updates the status to rewarded.
    /// </summary>
    /// <param name="referralId">The identifier of the referral to update.</param>
    /// <returns>The result of the operation, detailed error codes and description.</returns>
    [HttpPut("{referralId:guid}/rewarded")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetReferralStatusAsRewarded([FromRoute] string referralId)
    {
        try
        {
            await this.mediator.Send(new MarkRewardedCommand(referralId)).ConfigureAwait(true);
            return this.NoContent();
        }
        catch (ArgumentNullException e)
        {
            return this.BadRequest(new { error = e.Message });
        }
        catch (ArgumentException e)
        {
            return this.NotFound(new { error = e.Message });
        }
    }

    /// <summary>
    /// Returns all detailed referral for the authenticated user.
    /// </summary>
    /// <param name="page">page number.</param>
    /// <param name="pageSize">number of elements per page.</param>
    /// <returns>the result object or the detailed error message.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReferralDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReferralsByUser([FromQuery] int page, [FromQuery] int pageSize)
    {
        var rawUserId = this.HttpContext.Items["UserId"]?.ToString();
        if (rawUserId is null)
        {
            return this.Unauthorized();
        }

        #pragma warning disable CS8604 // Possible null reference argument.
        Guid userId = Guid.Parse(rawUserId);
        #pragma warning restore CS8604 // Possible null reference argument.

        try
        {
            var query = new ListReferralsByUserQuery(userId, page, pageSize);
            List<ReferralDto>? result =
                [.. await this.mediator.Send(query).ConfigureAwait(true)];
            return result is null ? this.NotFound() : this.Ok(result);
        }
        catch (Exception e) when (e is ArgumentNullException || e is ArgumentException)
        {
            return this.BadRequest(new { error = e.Message });
        }
    }
}