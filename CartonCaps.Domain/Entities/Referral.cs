namespace CartonCaps.Domain.Entities;

using CartonCaps.Domain.Enums;
using CartonCaps.Domain.Exceptions;
using CartonCaps.Domain.ValueObjects;

/// <summary>
/// Represents a referral entity within the domain.
/// </summary>
public class Referral(
    ReferralId id,
    ReferralCode code,
    TrackingId trackingId,
    ReferralStatus status,
    DateTime createdAt)
{
    /// <summary>
    /// Gets the unique identifier of this class.
    /// </summary>
    public ReferralId Id { get; } = id;

    /// <summary>
    ///  Gets the referral code for this referral.
    /// </summary>
    public ReferralCode Code { get; } = code;

    /// <summary>
    /// Gets the tracking ID for this referral.
    /// </summary>
    public TrackingId TrackingId { get; } = trackingId;

    /// <summary>
    /// Gets the timestamp this referral was created.
    /// </summary>
    public DateTime CreatedAt { get; } = createdAt;

    /// <summary>
    /// Gets the referral status.
    /// </summary>
    public ReferralStatus Status { get; private set; } = status;

    /// <summary>
    /// Gets the email of the referred user, if registered.
    /// </summary>
    public Email? ReferredEmail { get; private set; }

    /// <summary>
    /// Factory method to create a new instance.
    /// </summary>
    /// <param name="code"> the referral code, it should be 6 alphanumeric characters.</param>
    /// <param name="trackingId"> the tracking ID for this referral.</param>
    /// <returns>a new instance of Referral.</returns>
    public static Referral NewReferral(ReferralCode code, TrackingId trackingId)
    {
        return new Referral(
            ReferralId.New(),
            code,
            trackingId,
            ReferralStatus.PENDING,
            DateTime.UtcNow);
    }

    /// <summary>
    /// Marks the referral as installed.
    /// </summary>
    /// <exception cref="InvalidStateTransitionException">When the current status is not PENDING.</exception>
    public void MarkInstalled()
    {
        if (this.Status != ReferralStatus.PENDING)
        {
            throw new InvalidStateTransitionException(this.Status, ReferralStatus.INSTALLED);
        }

        this.Status = ReferralStatus.INSTALLED;
    }

    /// <summary>
    /// Marks the referral as registered.
    /// </summary>
    /// <param name="referredEmail">The email of the referred user.</param>
    /// <exception cref="InvalidStateTransitionException">When the current status is not INSTALLED.</exception>
    public void MarkRegistered(Email referredEmail)
    {
        if (this.Status != ReferralStatus.INSTALLED)
        {
            throw new InvalidStateTransitionException(this.Status, ReferralStatus.REGISTERED);
        }

        if (this.ReferredEmail != null)
        {
            throw new InvalidStateTransitionException(this.Status, ReferralStatus.REGISTERED);
        }

        this.ReferredEmail = referredEmail;
        this.Status = ReferralStatus.REGISTERED;
    }

    /// <summary>
    /// Marks the referral as rewarded.
    /// </summary>
    /// <exception cref="InvalidStateTransitionException">When the current status is not REGISTERED.</exception>
    public void MarkRewarded()
    {
        if (this.Status != ReferralStatus.REGISTERED)
        {
            throw new InvalidStateTransitionException(this.Status, ReferralStatus.REWARDED);
        }

        this.Status = ReferralStatus.REWARDED;
    }
}