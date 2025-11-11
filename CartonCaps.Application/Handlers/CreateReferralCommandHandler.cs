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
public class CreateReferralCommandHandler(IReferralRepository repository)
    : IRequestHandler<CreateReferralCommand, Guid>
{
    private readonly IReferralRepository repository = repository;

    /// <summary>
    /// Actual logic for the update of the datastore.
    /// </summary>
    /// <param name="request">The bundle of the required fields to process the usecase.</param>
    /// <param name="cancellationToken">fallback to cancell this async function.</param>
    /// <returns> the identifier of the newly created referral.</returns>
    /// <exception cref="ApplicationException"> when there is already a referral with the same Tracking ID.</exception>
    public async Task<Guid> Handle(
        CreateReferralCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var trackingId = new TrackingId(request.TrackingId);

        Referral? existingReferral = await this.repository.GetByTrackingIdAsync(trackingId).ConfigureAwait(false);
        if (existingReferral is not null)
        {
            #pragma warning disable CA2201 // Do not raise reserved exception types
            throw new ApplicationException("Referral with this Tracking ID already exists.");
            #pragma warning restore CA2201 // Do not raise reserved exception types
        }

        Referral newReferral = Referral.NewReferral(new (request.Code), trackingId);

        await this.repository.AddAsync(newReferral).ConfigureAwait(false);

        return newReferral.Id.Value;
    }
}