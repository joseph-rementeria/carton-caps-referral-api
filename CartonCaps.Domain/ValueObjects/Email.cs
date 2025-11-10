using System.Net.Mail;
using CartonCaps.Domain.Exceptions;
namespace CartonCaps.Domain.ValueObjects;
public record Email
{
    public string EmailString { get; }
    public Email(string emailString)
    {
        ArgumentException.ThrowIfNullOrEmpty(emailString, nameof(emailString));
        var lowerCaseEmail = emailString.ToLowerInvariant();
        try
        {
            _ = new MailAddress(lowerCaseEmail);
        }
        catch (FormatException ex)
        {
            throw new InvalidEmailAddressException(emailString, ex);
        }
        EmailString = lowerCaseEmail;
    }
}