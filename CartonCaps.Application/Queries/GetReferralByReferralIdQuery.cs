namespace CartonCaps.Application.Queries;

using MediatR;

/// <summary>
/// Query to get referral by Referral id.
/// </summary>
/// <param name="ReferralId">The guid string for the internal Referral</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record GetReferralByReferralIdQuery(string ReferralId)
    : IRequest<ReferralDto?>;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter