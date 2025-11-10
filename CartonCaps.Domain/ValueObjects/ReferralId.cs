namespace CartonCaps.Domain.ValueObjects;

using CartonCaps.Domain.Exceptions;

public record ReferralId
{
    /// <summary>
    /// Gets the referral identifier value.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReferralId"/> class.
    /// </summary>
    /// <param name="referralId">The referral identifier value.</param>
    /// <exception cref="InvalidReferralIdException">Thrown when the referral identifier is not a valid GUID.</exception>
    public ReferralId(Guid referralId)
    {
        if (referralId == Guid.Empty)
        {
            throw new InvalidReferralIdException(referralId.ToString());
        }

        this.Value = referralId;
    }

    /// <summary>
    /// Creates a new ReferralId with a new GUID value.
    /// </summary>
    /// <returns> the newly created ReferralId.</returns>
    public static ReferralId New() => new (Guid.NewGuid());

    /// <summary>
    /// Converts the ReferralId to a GUID.
    /// </summary>
    /// <returns>The GUID value.</returns>
    public Guid ToGuid() => this.Value;

    /// <summary>
    /// Creates a ReferralId from a GUID.
    /// </summary>
    /// <param name="from">The GUID value.</param>
    /// <returns>A new ReferralId instance.</returns>
    public static ReferralId FromGuid(Guid from) => new (from);

    public static implicit operator Guid(ReferralId id)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        return id.Value;
    }

    public static implicit operator ReferralId(Guid from) => new (from);
}