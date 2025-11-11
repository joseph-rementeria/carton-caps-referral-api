namespace CartonCaps.Application.Commands;

using MediatR;

/// <summary>
/// Bundle with the data required to mark a referral as registered.
/// </summary>
/// <param name="ReferralId">The referral ID guid for external requirements</param>
/// <param name="Email">The email address of the referred user</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record MarkRegisteredCommand(string ReferralId, string Email)
    : IRequest;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter