namespace CartonCaps.Application.Commands;

using MediatR;

/// <summary>
/// Command to initiate a new referral.
/// </summary>
/// <param name="Code">The 6 alphanumeric char referral code.</param>
/// <param name="TrackingId">The external system's GUID tracking identifier.</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record CreateReferralCommand(string Code, string TrackingId)
    : IRequest<Guid>
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
{
}