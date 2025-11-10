using CartonCaps.Domain.Exceptions;
namespace CartonCaps.Domain.ValueObjects;
public record ReferralId
{
    public Guid Value { get; }
    public ReferralId(Guid referralId)
    {
        if (referralId == Guid.Empty)
            throw new InvalidReferralIdException(referralId.ToString());
        Value = referralId;
    }
    public static ReferralId New() => new(Guid.NewGuid());

    public static implicit operator Guid(ReferralId id) => id.Value;
    public static implicit operator ReferralId(Guid guid) => new(guid);
}