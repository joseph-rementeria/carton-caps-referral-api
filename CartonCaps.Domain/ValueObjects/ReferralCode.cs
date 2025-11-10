namespace CartonCaps.Domain.ValueObjects;

using CartonCaps.Domain.Exceptions;

public record ReferralCode
{
    /// <summary>
    /// Gets the referral code string.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReferralCode"/> class.
    /// </summary>
    /// <param name="code">The referral code string.</param>
    /// <exception cref="InvalidReferralCodeException">Thrown when the code is not 6 alphanumeric chars.</exception>
    public ReferralCode(string code)
    {
        ArgumentException.ThrowIfNullOrEmpty(code, nameof(code));
        if (code.Length != 6 || code.Any(c => !char.IsLetterOrDigit(c)))
        {
            throw new InvalidReferralCodeException(code);
        }

        this.Code = code;
    }
}