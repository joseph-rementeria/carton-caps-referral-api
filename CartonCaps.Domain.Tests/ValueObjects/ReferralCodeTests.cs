namespace CartonCaps.Domain.Tests.ValueObjects;

using Xunit;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Exceptions;

/// <summary>
/// This class contains the tests done for VO ReferralCode.
/// </summary>
public class ReferralCodeTests
{
    /// <summary>
    /// Bundle of tests to verify multiple invalid inputs for ReferralCode VO.
    /// </summary>
    /// <param name="invalidCode">A string with an invalid referral code.</param>
    [Theory]
    [InlineData("-123")]
    [InlineData("AW5I")]
    [InlineData("AW5#")]
    [InlineData("AW588311")]
    [InlineData(" ")]
    public void ReferralCodeInvalidInput(string invalidCode)
    {
        Assert.Throws<InvalidReferralCodeException>(() => new ReferralCode(invalidCode));
    }

    /// <summary>
    /// Test the ability of the VO referral code to throw exception on invalid input.
    /// </summary>
    /// <param name="invalidReferralCode">either empty or null string.</param>
    [Fact]
    public void ReferralCodeInvalidOnEmpty()
    {
        Assert.Throws<ArgumentException>(() => new ReferralCode(string.Empty));
    }

    /// <summary>
    /// Test the ability of the VO email to trhow exception on invalid input.
    /// </summary>
    [Fact]
    public void ReferralCodeInvalidOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ReferralCode(null!));
    }

    /// <summary>
    /// Tests the happypath creation of a valid ReferralCode VO.
    /// </summary>
    [Fact]
    public void ReferralCodeValidInput()
    {
        var validCode = "AW5883";
        Assert.Equal(validCode, new ReferralCode(validCode).Code);
    }
}