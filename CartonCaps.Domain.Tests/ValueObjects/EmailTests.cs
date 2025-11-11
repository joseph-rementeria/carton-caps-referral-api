namespace CartonCaps.Domain.Tests.ValueObjects;

using Xunit;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Exceptions;

/// <summary>
/// This class contains all the test for the classes considered as Value Objects,
/// i.e., ReferrelId, TrackingId, Email and ReferralCode.
/// </summary>
public class EmailTests
{
    /// <summary>
    /// Test the ability of the VO email to trhow exception on invalid input.
    /// </summary>
    /// <param name="invalidEmail">a string containing an invalid email address.</param>
    [Theory]
    [InlineData("thisisnotanemail")]
    [InlineData("isthis.")]
    [InlineData(".john@doe")]
    [InlineData("@domain.com")]
    [InlineData(" ")]
    public void EmailInvalidOnInput(string invalidEmail)
    {
        Assert.Throws<InvalidEmailAddressException>(() => new Email(invalidEmail));
    }

    /// <summary>
    /// Test the ability of the VO email to trhow exception on invalid input.
    /// </summary>
    /// <param name="invalidEmail">either empty or null string.</param>
    [Fact]
    public void EmailInvalidOnEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Email(string.Empty));
    }

    /// <summary>
    /// Test the ability of the VO email to trhow exception on invalid input.
    /// </summary>
    [Fact]
    public void EmailInvalidOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => new Email(null!));
    }

    /// <summary>
    /// Tests the lowercase conversion of the email input.
    /// </summary>
    [Fact]
    public void EmailLowercased()
    {
        Assert.Equal("john.doe@domain.com", new Email("john.DOE@domain.COM").EmailString);
    }
}