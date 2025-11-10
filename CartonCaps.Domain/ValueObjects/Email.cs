namespace CartonCaps.Domain.ValueObjects;

using System.Net.Mail;
using CartonCaps.Domain.Exceptions;

public record Email
{
    /// <summary>
    /// Gets the email address as a string.
    /// </summary>
    public string EmailString { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="emailString">The raw email address.</param>
    /// <exception cref="InvalidEmailAddressException">If the string is not a valid email.</exception>
    public Email(string emailString)
    {
        ArgumentException.ThrowIfNullOrEmpty(emailString, nameof(emailString));

        // Email addresses are normally lowercase, so go with uppercase does not make sense, regardless of the performance tradeoff.
        #pragma warning disable CA1308
        var lowerCaseEmail = emailString.ToLowerInvariant();
        #pragma warning restore CA1308
        try
        {
            _ = new MailAddress(lowerCaseEmail);
        }
        catch (FormatException ex)
        {
            throw new InvalidEmailAddressException(emailString, ex);
        }

        this.EmailString = lowerCaseEmail;
    }
}