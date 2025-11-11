namespace CartonCaps.Application.Handlers;

using MediatR;
using CartonCaps.Application.Queries;
using CartonCaps.Domain.Repositories;
using CartonCaps.Domain.Entities;

/// <summary>
/// Handles the query to list user owned referrals, paginated.
/// </summary>
public class ListReferralsByUserQueryHandler(IReferralRepository repository)
    : IRequestHandler<ListReferralsByUserQuery, IEnumerable<ReferralDto>?>
{
    private readonly IReferralRepository repository = repository;

    /// <summary>
    /// implementation of the mediatR handler method.
    /// </summary>
    /// <param name="request">Bundle with the query parameters.</param>
    /// <param name="cancellationToken">Token to cancel current operation.</param>
    /// <returns>The list of referrals created by the user.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If page or size is off limits.</exception>
    public async Task<IEnumerable<ReferralDto>?> Handle(
        ListReferralsByUserQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        if (request.Page < 1)
        {
            // In this case, the correct parameter name is page rather than request due to the DDD design.
            #pragma warning disable CA2208 // Instantiate argument exceptions correctly
            throw new ArgumentOutOfRangeException(nameof(request.Page));
            #pragma warning restore CA2208 // Instantiate argument exceptions correctly
        }

        if (request.PageSize < 1 || request.PageSize > 100)
        {
            // In this case, the correct parameter name is size rather than request due to the DDD design.
            #pragma warning disable CA2208 // Instantiate argument exceptions correctly
            throw new ArgumentOutOfRangeException(nameof(request.PageSize));
            #pragma warning restore CA2208 // Instantiate argument exceptions correctly
        }

        IEnumerable<Referral>? referrals = await this.repository.ListByUserAsync(
            request.UserId,
            request.Page,
            request.PageSize).ConfigureAwait(false);

        return referrals is null ? null :
        [..
            referrals.Select(r => new ReferralDto(
                r.Id.Value,
                r.Code.Code,
                r.TrackingId.Value,
                r.Status,
                r.ReferredEmail?.EmailString,
                r.CreatedAt))
        ];
    }
}