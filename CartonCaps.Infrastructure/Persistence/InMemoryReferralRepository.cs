namespace CartonCaps.Infrastructure.Persistence;

using CartonCaps.Domain.Entities;
using CartonCaps.Domain.Repositories;
using CartonCaps.Domain.ValueObjects;

/// <summary>
/// In memory implementatio of the repo, in this case this is a testing module.
/// WARNING: This is not intended for prd, only for testing pursoses.
/// </summary>
public class InMemoryReferralRepository : IReferralRepository
{
    // Static List for in-memory storage. All requests use the same list instance.
    private readonly List<Referral> referrals =
        [
            Referral.NewReferral(
                new ReferralCode("A1B2C3"),
                new TrackingId("1a4b3c4d-5e6f-7080-9102-34567890abcd")),
            Referral.NewReferral(
                new ReferralCode("A1B2C3"),
                new TrackingId("1a2b7c4d-5e6f-7080-9102-34567890abcd")),
            Referral.NewReferral(
                new ReferralCode("A1B2C3"),
                new TrackingId("1a2b3c4d-8e6f-7080-9102-34567890abcd")),
            Referral.NewReferral(
                new ReferralCode("A1B2C3"),
                new TrackingId("1a2b3c4d-5e6f-9080-9102-34567890abcd")),
            Referral.NewReferral(
                new ReferralCode("A1B2C3"),
                new TrackingId("1a2b3c4d-5e6f-7080-0102-34567890abcd")),
            Referral.NewReferral(
                new ReferralCode("A1B2C3"),
                new TrackingId("1a2b3c4d-5e6f-7080-9102-34567890abcd")),
        ];

    /// <summary>
    /// Updates the in memory list.
    /// </summary>
    /// <param name="referral">object to save.</param>
    /// <returns>If the element is not in the list.</returns>
    /// <exception cref="InvalidOperationException">When element is already in the list.</exception>
    public Task AddAsync(Referral referral)
    {
        ArgumentNullException.ThrowIfNull(referral);
        if (this.referrals.Any(r => r.Id.Value == referral.Id.Value))
        {
            throw new InvalidOperationException($"Referral ID {referral.Id.Value} already exists");
        }

        this.referrals.Add(referral);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets by PK.
    /// </summary>
    /// <param name="id">valid ReferralId.</param>
    /// <returns>the element if found.</returns>
    public Task<Referral?> GetByIdAsync(ReferralId id)
    {
        // Find by the internal primary key
        Referral? referral = this.referrals.FirstOrDefault(r => r.Id.Value == id.Value);
        return Task.FromResult(referral);
    }

    /// <summary>
    /// Gets by trackingID.
    /// </summary>
    /// <param name="trackingId">valid TrackingId.</param>
    /// <returns>the element if found.</returns>
    public Task<Referral?> GetByTrackingIdAsync(TrackingId trackingId)
    {
        // Find by the external tracking ID
        Referral? referral = this.referrals.FirstOrDefault(r => r.TrackingId.Value == trackingId.Value);
        return Task.FromResult(referral);
    }

    /// <summary>
    /// Updates if present in the list.
    /// </summary>
    /// <param name="referral">The new object to replace.</param>
    /// <returns>if done succesfully.</returns>
    /// <exception cref="InvalidOperationException">if not present.</exception>
    public Task UpdateAsync(Referral referral)
    {
        ArgumentNullException.ThrowIfNull(referral);
        if (!this.referrals.Any(r => r.Id.Value == referral.Id.Value))
        {
            throw new InvalidOperationException($"Referral ID {referral.Id.Value} not found.");
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Returns a paginated list of the referrals.
    /// </summary>
    /// <param name="userId">the user ID to filter referrals.</param>
    /// <param name="page">the page number to retrieve.</param>
    /// <param name="pageSize">the number of referrals per page.</param>
    /// <returns>a paginated list of referrals for the specified user.</returns>
    public Task<IEnumerable<Referral>> ListByUserAsync(Guid userId, int page, int pageSize)
    {
        // here we are not using the user id as it is mocked data.
        // Real implementation would do a join with the user table
        // and a N:M relatioship to get the data per user.
        IEnumerable<Referral> results =
        [.. this.referrals
            .OrderByDescending(r => r.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)];

        return Task.FromResult(results);
    }
}