namespace CartonCaps.Application.Commands;

using MediatR;

/// <summary>
/// Bundle with the data required to mark a referral as installed.
/// </summary>
/// <param name="TrackingId">The tracking ID guid for external requirements</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record MarkInstalledCommand(string TrackingId)
    : IRequest;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter