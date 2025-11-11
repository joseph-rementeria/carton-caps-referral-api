namespace CartonCaps.Domain.Tests.ValueObjects;

using Xunit;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Exceptions;

/// <summary>
/// This class contains the tests done for VO TrackingId.
/// </summary>
public class TrackingIdTests
{
    /// <summary>
    /// Bundle of tests to verify multiple invalid inputs for TrackingId VO.
    /// </summary>
    /// <param name="invalidCode">A string with an invalid referral code.</param>
    [Theory]
    [InlineData("-123")]
    [InlineData("AW5I")]
    [InlineData("AW5#")]
    [InlineData("AW588311")]
    [InlineData(" ")]
    [InlineData("not-a-guid")]
    [InlineData("1a2b3c4d-5e6f-7080-9102-34567890abcd-extra")]
    public void TrackingIdInvalidInput(string invalidCode)
    {
        Assert.Throws<InvalidTrackingIdException>(() => new TrackingId(invalidCode));
    }

    /// <summary>
    /// Test the ability of the VO referral code to throw exception on invalid input.
    /// </summary>
    /// <param name="invalidTrackingId">either empty or null string.</param>
    [Fact]
    public void TrackingIdInvalidOnEmpty()
    {
        Assert.Throws<ArgumentException>(() => new TrackingId(string.Empty));
    }

    /// <summary>
    /// Test the ability of the VO email to trhow exception on invalid input.
    /// </summary>
    [Fact]
    public void TrackingIdInvalidOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => new TrackingId(null!));
    }

    /// <summary>
    /// Tests the happypath creation of a valid TrackingId VO.
    /// </summary>
    [Fact]
    public void TrackingIdValidInput()
    {
        Assert.NotNull(new TrackingId("1a2b3c4d-5e6f-7080-9102-34567890abcd"));
    }
}