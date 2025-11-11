namespace CartonCaps.Application.Queries;

using MediatR;

/// <summary>
/// Query to find the paginated list of referrals for a given user.
/// </summary>
/// <param name="UserId">the identifier of the user owning the referrals.</param>
/// <param name="Page">The page number (default 1).</param>
/// <param name="PageSize">The number of items per page (default 25).</param>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record ListReferralsByUserQuery(Guid UserId, int Page = 1, int PageSize = 25)
    : IRequest<IEnumerable<ReferralDto>>;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter