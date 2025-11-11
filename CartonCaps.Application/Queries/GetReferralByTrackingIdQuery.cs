namespace CartonCaps.Application.Queries;

using MediatR;

/// <summary>
/// Query to get referral by tracking id.
/// </summary>
/// <param name="TrackingId">The guid string for the external tracking</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record GetReferralByTrackingIdQuery(string TrackingId)
    : IRequest<ReferralDto?>;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter