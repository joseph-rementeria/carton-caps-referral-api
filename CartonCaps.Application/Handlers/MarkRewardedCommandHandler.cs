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
public class MarkRewardedCommandHandler(IReferralRepository repository)
    : IRequestHandler<MarkRewardedCommand>
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
        MarkRewardedCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var referralId = new ReferralId(Guid.Parse(request.ReferralId));
        Referral? referral = await this.repository.GetByIdAsync(referralId).ConfigureAwait(false) ??
            throw new ArgumentException($"Referral ID '{request.ReferralId}' not found.");
        referral.MarkRewarded();
        await this.repository.UpdateAsync(referral).ConfigureAwait(false);
    }
}