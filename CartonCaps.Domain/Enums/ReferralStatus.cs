namespace CartonCaps.Domain.Enums;

/// <summary>
/// Represents the status of a referral in the system.
/// </summary>
public enum ReferralStatus
{
    /// <summary>
    /// The referred user has not yet open the link.
    /// </summary>
    PENDING = 0,

    /// <summary>
    /// The referred user has opened the link and installed the app.
    /// </summary>
    INSTALLED = 1,

    /// <summary>
    /// The referred user has registered or linked an account.
    /// </summary>
    REGISTERED = 2,

    /// <summary>
    /// The referred user has been rewarded.
    /// </summary>
    REWARDED = 3,
}