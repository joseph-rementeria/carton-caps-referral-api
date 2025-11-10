using CartonCaps.Domain.Exceptions;
namespace CartonCaps.Domain.ValueObjects;
public record ReferralCode
{
    public string Code { get; }
    public ReferralCode(string code)
    {
        ArgumentException.ThrowIfNullOrEmpty(code, nameof(code));
        if (code.Length != 6 || code.Any(c => !char.IsLetterOrDigit(c)))
            throw new InvalidReferralCodeException(code);
        Code = code;
    }
}