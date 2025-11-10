using CartonCaps.Domain.Enums;
namespace CartonCaps.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
    protected DomainException(string message, Exception innerException) : base(message, innerException) { }
}

public class InvalidReferralCodeException(string code) : DomainException($"Invalid Referral code '{code}', it must be 6 alphanumeric characters")
{
}

public class InvalidTrackingIdException(string trackingId) : DomainException($"Invalid Tracking ID '{trackingId}, not in GUID format")
{
}

public class InvalidEmailAddressException(string email, Exception innerException) : DomainException($"Invalid email address '{email}'", innerException)
{
}

public class InvalidReferralIdException(string referralId) : DomainException($"Invalid Referral ID {referralId}, not in GUID format")
{
}

public class InvalidStateTransitionException(ReferralStatus currentStatus, ReferralStatus targetStatus)
    : DomainException($"Invalid referral transition from '{currentStatus}' to '{targetStatus}'.")
{
}