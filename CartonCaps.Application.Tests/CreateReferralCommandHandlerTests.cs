namespace CartonCaps.Application.Tests;

using Moq;
using Xunit;
using CartonCaps.Application.Commands;
using CartonCaps.Application.Handlers;
using CartonCaps.Domain.Repositories;
using CartonCaps.Domain.Entities;
using CartonCaps.Domain.ValueObjects;

/// <summary>
/// Tests for the CreateReferralCommandHandler.
/// </summary>
public class CreateReferralCommandHandlerTests
{
    // Mock the IReferralRepository interface
    private readonly Mock<IReferralRepository> mockRepo = new ();
    private readonly CreateReferralCommandHandler handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReferralCommandHandlerTests"/> class.
    /// </summary>
    public CreateReferralCommandHandlerTests()
    {
        this.handler = new CreateReferralCommandHandler(this.mockRepo.Object);
    }

    /// <summary>
    /// Tests that usecase fails if referral is already created.
    /// </summary>
    /// <returns>Async task representing the execution, mandatory due to been async method.</returns>
    [Fact]
    public async Task HandlerThrowsErrorBecauseItIsCreatedAlready()
    {
        var trackingGuid = Guid.NewGuid();
        var referral = Referral.NewReferral(new ReferralCode("A1B2C3"), new TrackingId(trackingGuid.ToString()));
        this.mockRepo
            .Setup(r => r.GetByTrackingIdAsync(It.Is<TrackingId>(id => id.Value == trackingGuid)))
            .ReturnsAsync(referral);

        Exception ex = await Record.ExceptionAsync(() => this.handler.Handle(new CreateReferralCommand("A1B2C3", trackingGuid.ToString()), CancellationToken.None)).ConfigureAwait(true);
        Assert.NotNull(ex);
        Assert.IsType<ApplicationException>(ex);
        this.mockRepo.Verify(r => r.AddAsync(It.IsAny<Referral>()), Times.Never());
    }

    /// <summary>
    /// Tests the handler's happypath.
    /// </summary>
    /// <returns>Async task representing the execution, mandatory due to been async method.</returns>
    [Fact]
    public async Task HandlerDuringHappypath()
    {
        var trackingGuid = Guid.NewGuid();
        var referral = Referral.NewReferral(new ReferralCode("A1B2C3"), new TrackingId(trackingGuid.ToString()));
        this.mockRepo
            .Setup(r => r.GetByTrackingIdAsync(It.Is<TrackingId>(id => id.Value == trackingGuid)))
            .ReturnsAsync((Referral?)null);

        Exception ex = await Record.ExceptionAsync(() => this.handler.Handle(new CreateReferralCommand("A1B2C3", trackingGuid.ToString()), CancellationToken.None)).ConfigureAwait(true);
        Assert.Null(ex);
        this.mockRepo.Verify(r => r.AddAsync(It.IsAny<Referral>()), Times.Once());
    }
}