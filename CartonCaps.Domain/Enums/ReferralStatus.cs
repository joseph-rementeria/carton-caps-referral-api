namespace CartonCaps.Domain.Enums;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the status of a referral in the system.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReferralStatus
{
    /// <summary>
    /// The referred user has not yet open the link.
    /// </summary>
    PENDING,

    /// <summary>
    /// The referred user has opened the link and installed the app.
    /// </summary>
    INSTALLED,

    /// <summary>
    /// The referred user has registered or linked an account.
    /// </summary>
    REGISTERED,

    /// <summary>
    /// The referred user has been rewarded.
    /// </summary>
    REWARDED,
}