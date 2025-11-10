using CartonCaps.Domain.Entities;
using CartonCaps.Domain.ValueObjects;


namespace CartonCaps.Domain.Repositories;

public interface IReferralRepository
{
    /// <summary>
    /// Looks for the referral object by ID.
    /// This is the PK of the referral entity, use primarily on internal transactions.
    /// </summary>
    Task<Referral?> GetByIdAsync(ReferralId id);

    /// <summary>
    /// Looks for the referral object by its tracking ID.
    /// This is only for external tracking purposes.
    /// </summary>
    Task<Referral?> GetByTrackingIdAsync(TrackingId trackingId);

    /// <summary>
    /// Persists a new Referral instance in the data store.
    /// </summary>
    Task AddAsync(Referral referral);

    /// <summary>
    /// Updates the existing Referral instance in the data store.
    /// </summary>
    Task UpdateAsync(Referral referral);

    /// <summary>
    /// Looks for a list of referrals that where created by an specific user
    /// UserId is assumed to be another Value Object/Identifier passed from the Application Layer.
    /// </summary>
    Task<IEnumerable<Referral>> ListByUserAsync(Guid userId, int page, int pageSize);
}