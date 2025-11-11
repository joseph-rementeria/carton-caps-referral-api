namespace CartonCaps.Application.Commands;

using MediatR;

/// <summary>
/// Bundle with the data required to mark a referral as rewarded.
/// </summary>
/// <param name="ReferralId">The referral ID guid for external requirements</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record MarkRewardedCommand(string ReferralId)
    : IRequest;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter