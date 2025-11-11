namespace CartonCaps.Domain.Tests.Entities;

using CartonCaps.Domain.Entities;
using CartonCaps.Domain.Enums;
using CartonCaps.Domain.Exceptions;
using CartonCaps.Domain.ValueObjects;

/// <summary>
/// This class contains the tests done for Entity Referral.
/// </summary>
public class ReferralTests
{
    private readonly ReferralCode testCode = new ("C4RT0N");
    private readonly TrackingId testTrackingId = new (Guid.NewGuid().ToString());
    private readonly Email testEmail = new ("user@test.com");

    /// <summary>
    /// Tests the happypath creation of a referral entity and its initial fields.
    /// </summary>
    [Fact]
    public void ReferralCreationHappypath()
    {
        var referral = Referral.NewReferral(this.testCode, this.testTrackingId);
        Assert.Equal(this.testCode, referral.Code);
        Assert.Equal(this.testTrackingId, referral.TrackingId);
        Assert.Equal(ReferralStatus.PENDING, referral.Status);
        Assert.Null(referral.ReferredEmail);
    }

    /// <summary>
    /// Tests the status change of the referral to registered.
    /// </summary>
    [Fact]
    public void ReferralHappypathLifeCycle()
    {
        var referral = Referral.NewReferral(this.testCode, this.testTrackingId);
        referral.MarkInstalled();
        Assert.Equal(ReferralStatus.INSTALLED, referral.Status);
        referral.MarkRegistered(this.testEmail);
        Assert.Equal(this.testEmail, referral.ReferredEmail);
        Assert.Equal(ReferralStatus.REGISTERED, referral.Status);
        referral.MarkRewarded();
        Assert.Equal(ReferralStatus.REWARDED, referral.Status);
    }

    /// <summary>
    /// Tests invalid status transitions from the PENDING state.
    /// </summary>
    [Fact]
    public void ReferralWrongLifeCycleFromPending()
    {
        var referral = Referral.NewReferral(this.testCode, this.testTrackingId);
        Assert.Throws<InvalidStateTransitionException>(() => referral.MarkRegistered(this.testEmail));
        Assert.Throws<InvalidStateTransitionException>(() => referral.MarkRewarded());
    }

    /// <summary>
    /// Tests invalid status transitions from the INSTALLED state.
    /// </summary>
    [Fact]
    public void ReferralWrongLifeCycleFromInstalled()
    {
        var referral = Referral.NewReferral(this.testCode, this.testTrackingId);
        referral.MarkInstalled();
        Assert.Throws<InvalidStateTransitionException>(() => referral.MarkRewarded());
    }
}