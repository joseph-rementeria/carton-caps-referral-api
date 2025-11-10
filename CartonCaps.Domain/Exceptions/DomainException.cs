namespace CartonCaps.Domain.Exceptions;

using CartonCaps.Domain.Enums;

/// <summary>
/// Base class for domain-specific exceptions.
/// </summary>
public abstract class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    protected DomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    protected DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    protected DomainException()
    {
    }
}

/// <summary>
/// Indicates that there was an error on the referral code format.
/// </summary>
/// <param name="code">The code that caused the error.</param>
public class InvalidReferralCodeException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidReferralCodeException"/> class.
    /// </summary>
    public InvalidReferralCodeException()
        : this(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidReferralCodeException"/> class.
    /// </summary>
    /// <param name="code">The code that caused the error.</param>
    public InvalidReferralCodeException(string code)
        : base($"Invalid Referral code '{code}', it must be 6 alphanumeric characters")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidReferralCodeException"/> class.
    /// </summary>
    /// <param name="message">The detailed message of what caused the error.</param>
    /// <param name="innerException">The exception that caused this in the first place.</param>
    public InvalidReferralCodeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Indicates that there was an error on the tracking ID format.
/// </summary>
public class InvalidTrackingIdException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTrackingIdException"/> class.
    /// </summary>
    public InvalidTrackingIdException()
        : base(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTrackingIdException"/> class.
    /// </summary>
    /// <param name="trackingId">The tracking ID that caused the error.</param>
    public InvalidTrackingIdException(string trackingId)
        : base($"Invalid Tracking ID '{trackingId}, not in GUID format")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTrackingIdException"/> class.
    /// </summary>
    /// <param name="trackingId">The tracking ID that caused the error.</param>
    /// <param name="innerException">The exception that caused this in the first place.</param>
    public InvalidTrackingIdException(string trackingId, Exception innerException)
        : base($"Invalid Tracking ID '{trackingId}, not in GUID format", innerException)
    {
    }
}

/// <summary>
/// Indicates that there was an error on the email address format.
/// </summary>
public class InvalidEmailAddressException : DomainException
{
    /// <summary>
    ///  Initializes a new instance of the <see cref="InvalidEmailAddressException"/> class.
    /// </summary>
    public InvalidEmailAddressException()
        : base(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailAddressException"/> class.
    /// </summary>
    /// <param name="email">The email address that caused the error.</param>
    public InvalidEmailAddressException(string email)
        : base($"Invalid email address '{email}'")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailAddressException"/> class.
    /// </summary>
    /// <param name="email">The email address that caused the error.</param>
    /// <param name="innerException">The exception that caused this in the first place.</param>
    public InvalidEmailAddressException(string email, Exception innerException)
        : base($"Invalid email address '{email}'", innerException)
    {
    }
}

/// <summary>
/// Indicates that there was an error on the referral ID format.
/// </summary>
public class InvalidReferralIdException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidReferralIdException"/> class.
    /// </summary>
    public InvalidReferralIdException()
        : base(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidReferralIdException"/> class.
    /// </summary>
    /// <param name="referralId">The invalid referral ID.</param>
    public InvalidReferralIdException(string referralId)
        : base($"Invalid Referral ID {referralId}, not in GUID format")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidReferralIdException"/> class.
    /// </summary>
    /// <param name="referralId">The invalid referral ID.</param>
    /// <param name="innerException">The exception that caused the issue in the first palce.</param>
    public InvalidReferralIdException(string referralId, Exception innerException)
        : base($"Invalid Referral ID {referralId}, not in GUID format", innerException)
    {
    }
}

/// <summary>
/// Indicates that there was an invalid state transition for a referral.
/// </summary>
public class InvalidStateTransitionException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidStateTransitionException"/> class.
    /// </summary>
    public InvalidStateTransitionException()
        : base(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidStateTransitionException"/> class.
    /// </summary>
    /// <param name="currentStatus">The current status.</param>
    /// <param name="targetStatus">The target invalid status.</param>
    public InvalidStateTransitionException(ReferralStatus currentStatus, ReferralStatus targetStatus)
        : base($"Invalid referral transition from '{currentStatus}' to '{targetStatus}'.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidStateTransitionException"/> class.
    /// </summary>
    /// <param name="message">The detailed error message.</param>
    public InvalidStateTransitionException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidStateTransitionException"/> class.
    /// </summary>
    /// <param name="message">The detailed error message.</param>
    /// <param name="innerException">The exception that caused the issue in the first place.</param>
    public InvalidStateTransitionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}