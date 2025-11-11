namespace CartonCaps.Application.Tests;

using Moq;
using Xunit;
using CartonCaps.Application.Queries;
using CartonCaps.Application.Handlers;
using CartonCaps.Domain.Repositories;

/// <summary>
/// Tests for GetReferralsByUserQueryHandler.
/// </summary>
public class ListReferralsByUserQueryHandlerTests
{
    private readonly Mock<IReferralRepository> mockRepo = new ();
    private readonly ListReferralsByUserQueryHandler handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListReferralsByUserQueryHandlerTests"/> class.
    /// </summary>
    public ListReferralsByUserQueryHandlerTests()
    {
        this.handler = new ListReferralsByUserQueryHandler(this.mockRepo.Object);
    }

    /// <summary>
    /// Tests handler parameter validation failure.
    /// </summary>
    /// <returns>Async task representing the execution, mandatory due to been async method.</returns>
    [Fact]
    public async Task HandlerParamsValidationFailure()
    {
        var userId = Guid.NewGuid();
        var page = -1;
        var size = 15;
        Exception ex = await Record.ExceptionAsync(() => this.handler.Handle(new ListReferralsByUserQuery(userId, page, size), CancellationToken.None)).ConfigureAwait(true);
        Assert.NotNull(ex);
        Assert.IsType<ArgumentOutOfRangeException>(ex);

        page = 1;
        size = -1;
        ex = await Record.ExceptionAsync(() => this.handler.Handle(new ListReferralsByUserQuery(userId, page, size), CancellationToken.None)).ConfigureAwait(true);
        Assert.NotNull(ex);
        Assert.IsType<ArgumentOutOfRangeException>(ex);
    }

    /// <summary>
    /// Tests handler happypath.
    /// </summary>
    /// <returns>Async task representing the execution, mandatory due to been async method.</returns>
    [Fact]
    public async Task HandlersHappypath()
    {
        var userId = Guid.NewGuid();
        var page = 1;
        var size = 15;
        Exception ex = await Record.ExceptionAsync(() => this.handler.Handle(new ListReferralsByUserQuery(userId, page, size), CancellationToken.None)).ConfigureAwait(true);
        Assert.Null(ex);
        this.mockRepo.Verify(r => r.ListByUserAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
    }
}