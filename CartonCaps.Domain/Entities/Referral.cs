using CartonCaps.Domain.Enums;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Exceptions;

namespace CartonCaps.Domain.Entities;

public class Referral(
    ReferralId id,
    ReferralCode code,
    TrackingId trackingId,
    ReferralStatus status,
    DateTime createdAt)
{
    public ReferralId Id { get; } = id;
    public ReferralCode Code { get; } = code;
    public TrackingId TrackingId { get; } = trackingId;
    public DateTime CreatedAt { get; } = createdAt;
    public ReferralStatus Status { get; private set; } = status;
    public Email? ReferredEmail { get; private set; } = null;

    public static Referral NewReferral(ReferralCode code, TrackingId trackingId)
    {
        return new Referral(
            ReferralId.New(),
            code,
            trackingId,
            ReferralStatus.PENDING,
            DateTime.UtcNow
        );
    }
    public void MarkInstalled()
    {
        if (Status != ReferralStatus.PENDING)
            throw new InvalidStateTransitionException(Status, ReferralStatus.INSTALLED);
        Status = ReferralStatus.INSTALLED;
    }

    public void MarkRegistered(Email referredEmail)
    {
        if (Status != ReferralStatus.INSTALLED)
            throw new InvalidStateTransitionException(Status, ReferralStatus.REGISTERED);
        if (ReferredEmail != null)
            throw new InvalidStateTransitionException(Status, ReferralStatus.REGISTERED);
        ReferredEmail = referredEmail;
        Status = ReferralStatus.REGISTERED;
    }

    public void MarkRewarded()
    {
        if (Status != ReferralStatus.REGISTERED)
            throw new InvalidStateTransitionException(Status, ReferralStatus.REWARDED);
        Status = ReferralStatus.REWARDED;
    }
}