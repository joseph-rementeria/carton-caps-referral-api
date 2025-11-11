namespace CartonCaps.Application.Queries;

using CartonCaps.Domain.Enums;

/// <summary>
/// DTO for referral info.
/// </summary>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record ReferralDto(
    Guid Id,
    string Code,
    Guid TrackingId,
    ReferralStatus Status,
    string? ReferredEmail,
    DateTime CreatedAt);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter