namespace CartonCaps.Application.Handlers;

using MediatR;
using CartonCaps.Application.Commands;
using CartonCaps.Domain.Repositories;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Entities;

/// <summary>
/// Handler for creating a referral.
/// </summary>
/// <param name="repository">autowired dependency to handle referral data operations.</param>
public class MarkInstalledCommandHandler(IReferralRepository repository)
    : IRequestHandler<MarkInstalledCommand>
{
    private readonly IReferralRepository repository = repository;

    /// <summary>
    /// Actual logic for the update of the datastore.
    /// </summary>
    /// <param name="request">The bundle of the required fields to process the usecase.</param>
    /// <param name="cancellationToken">fallback to cancell this async function.</param>
    /// <returns> the identifier of the newly created referral.</returns>
    /// <exception cref="ApplicationException"> when there is already a referral with the same Tracking ID.</exception>
    public async Task Handle(
        MarkInstalledCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var trackingId = new TrackingId(request.TrackingId);
        Referral? referral = await this.repository.GetByTrackingIdAsync(trackingId).ConfigureAwait(false) ??
            throw new ArgumentException($"Tracking ID '{request.TrackingId}' not found.");
        referral.MarkInstalled();
        await this.repository.UpdateAsync(referral).ConfigureAwait(false);
    }
}