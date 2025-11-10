namespace CartonCaps.Domain.ValueObjects;

using CartonCaps.Domain.Exceptions;

public record TrackingId
{
    /// <summary>
    /// Gets the tracking identifier value.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrackingId"/> class.
    /// </summary>
    /// <param name="trackingId">The tracking identifier value.</param>
    /// <exception cref="InvalidTrackingIdException">Thrown when the tracking identifier is not a valid GUID.</exception>
    public TrackingId(string trackingId)
    {
        ArgumentException.ThrowIfNullOrEmpty(trackingId, nameof(trackingId));
        if (!Guid.TryParse(trackingId, out Guid result))
        {
            throw new InvalidTrackingIdException(trackingId);
        }

        this.Value = result;
    }

    /// <summary>
    /// Converts the TrackingId to a GUID.
    /// </summary>
    /// <param name="id">The TrackingId instance.</param>
    /// <returns>The GUID value.</returns>
    public static Guid ToGuid(TrackingId id)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        return id.Value;
    }

    public static implicit operator Guid(TrackingId id) => ToGuid(id);
}