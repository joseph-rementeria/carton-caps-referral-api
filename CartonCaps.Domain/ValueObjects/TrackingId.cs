using CartonCaps.Domain.Exceptions;
namespace CartonCaps.Domain.ValueObjects;
public record TrackingId
{
    public Guid Value { get; }
    public TrackingId(string trackingId)
    {
        ArgumentException.ThrowIfNullOrEmpty(trackingId, nameof(trackingId));
        if (!Guid.TryParse(trackingId, out Guid result))
            throw new InvalidTrackingIdException(trackingId);
        Value = result;
    }
}