namespace CartonCaps.Domain.Tests.ValueObjects;

using Xunit;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Exceptions;

/// <summary>
/// This class contains the tests done for VO ReferralId.
/// </summary>
public class ReferralIdTests
{
    /// <summary>
    /// Testing what happends when an invalid input is provided to ReferralId VO.
    /// </summary>
    [Fact]
    public void ReferralIdInvalidInput()
    {
        Assert.Throws<InvalidReferralIdException>(() => new ReferralId(Guid.Empty));
    }
}