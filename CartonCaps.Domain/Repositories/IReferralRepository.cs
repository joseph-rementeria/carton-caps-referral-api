namespace CartonCaps.Domain.Repositories;

using CartonCaps.Domain.Entities;
using CartonCaps.Domain.ValueObjects;

/// <summary>
/// Repository interface for managing Referral entities.
/// </summary>
public interface IReferralRepository
{
    /// <summary>
    /// Looks for the referral object by ID.
    /// This is the PK of the referral entity, use primarily on internal transactions.
    /// </summary>
    /// <param name="id">The identifier of the referral.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<Referral?> GetByIdAsync(ReferralId id);

    /// <summary>
    /// Looks for the referral object by its tracking ID.
    /// This is only for external tracking purposes.
    /// </summary>
    /// <param name="trackingId">The tracking ID of the referral.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<Referral?> GetByTrackingIdAsync(TrackingId trackingId);

    /// <summary>
    /// Persists a new Referral instance in the data store.
    /// </summary>
    /// <param name="referral">The referral entity to add.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddAsync(Referral referral);

    /// <summary>
    /// Updates the existing Referral instance in the data store.
    /// </summary>
    /// <param name="referral">The referral entity to update.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(Referral referral);

    /// <summary>
    /// Looks for a list of referrals that where created by an specific user
    /// UserId is assumed to be another Value Object/Identifier passed from the Application Layer.
    /// </summary>
    /// <param name="userId">The identifier of the user who created the referrals.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page for pagination.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<IEnumerable<Referral>> ListByUserAsync(Guid userId, int page, int pageSize);
}