namespace CartonCaps.Application.Handlers;

using MediatR;
using CartonCaps.Application.Queries;
using CartonCaps.Domain.Repositories;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Entities;

/// <summary>
/// Handles the query to get referral by Referral ID.
/// </summary>
public class GetReferralByReferralIdQueryHandler(IReferralRepository repository)
    : IRequestHandler<GetReferralByReferralIdQuery, ReferralDto?>
{
    private readonly IReferralRepository repository = repository;

    /// <summary>
    /// Handles the fetching of the referral by Referral ID.
    /// </summary>
    /// <param name="request">bundle with info needed for the query.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The referral object based on it Referral ID.</returns>
    public async Task<ReferralDto?> Handle(
        GetReferralByReferralIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var referralId = new ReferralId(Guid.Parse(request.ReferralId));
        Referral? referral = await this.repository.GetByIdAsync(referralId).ConfigureAwait(false);

        return referral is null ? null : new ReferralDto(
            referral.Id.Value,
            referral.Code.Code,
            referral.TrackingId.Value,
            referral.Status,
            referral.ReferredEmail?.EmailString,
            referral.CreatedAt);
    }
}