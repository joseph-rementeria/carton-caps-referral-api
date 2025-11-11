namespace CartonCaps.Application.Handlers;

using MediatR;
using CartonCaps.Application.Queries;
using CartonCaps.Domain.Repositories;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Entities;

/// <summary>
/// Handles the query to get referral by tracking ID.
/// </summary>
public class GetReferralByTrackingIdQueryHandler(IReferralRepository repository)
    : IRequestHandler<GetReferralByTrackingIdQuery, ReferralDto?>
{
    private readonly IReferralRepository repository = repository;

    /// <summary>
    /// Handles the fetching of the referral by tracking ID.
    /// </summary>
    /// <param name="request">bundle with info needed for the query.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The referral object based on it tracking ID.</returns>
    public async Task<ReferralDto?> Handle(
        GetReferralByTrackingIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var trackingId = new TrackingId(request.TrackingId);
        Referral? referral = await this.repository.GetByTrackingIdAsync(trackingId).ConfigureAwait(false);

        return referral is null ? null : new ReferralDto(
            referral.Id.Value,
            referral.Code.Code,
            referral.TrackingId.Value,
            referral.Status,
            referral.ReferredEmail?.EmailString,
            referral.CreatedAt);
    }
}