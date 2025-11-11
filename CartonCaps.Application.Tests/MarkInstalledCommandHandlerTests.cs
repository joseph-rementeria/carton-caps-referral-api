namespace CartonCaps.Application.Tests;

using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using CartonCaps.Application.Commands;
using CartonCaps.Application.Handlers;
using CartonCaps.Domain.Repositories;
using CartonCaps.Domain.Entities;
using CartonCaps.Domain.ValueObjects;
using CartonCaps.Domain.Enums;

/// <summary>
/// Tests for the MarkInstalledCommandHandler.
/// </summary>
public class MarkInstalledCommandHandlerTests
{
    // Mock the IReferralRepository interface
    private readonly Mock<IReferralRepository> mockRepo = new ();
    private readonly MarkInstalledCommandHandler handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkInstalledCommandHandlerTests"/> class.
    /// </summary>
    public MarkInstalledCommandHandlerTests()
    {
        this.handler = new MarkInstalledCommandHandler(this.mockRepo.Object);
    }

    /// <summary>
    /// Tests the happypath of the handler.
    /// </summary>
    /// <returns>Async task representing the execution, mandatory due to been async method.</returns>
    [Fact]
    public async Task HandlerHappypath()
    {
        var trackingGuid = Guid.NewGuid();
        var referral = Referral.NewReferral(new ReferralCode("A1B2C3"), new TrackingId(trackingGuid.ToString()));
        this.mockRepo
            .Setup(r => r.GetByTrackingIdAsync(It.Is<TrackingId>(id => id.Value == trackingGuid)))
            .ReturnsAsync(referral);

        Exception ex = await Record.ExceptionAsync(() => this.handler.Handle(new MarkInstalledCommand(trackingGuid.ToString()), CancellationToken.None)).ConfigureAwait(true);
        Assert.Null(ex);
        this.mockRepo.Verify(r => r.GetByTrackingIdAsync(It.IsAny<TrackingId>()), Times.Once());
        this.mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Referral>()), Times.Once());
        Assert.Equal(ReferralStatus.INSTALLED, referral.Status);
    }

    /// <summary>
    /// Tests that the handler throws errors when no tracking id is found.
    /// </summary>
    /// <returns>Async task representing the execution, mandatory due to been async method.</returns>
    [Fact]
    public async Task HandlerThrowsErrorsWhenNoTrackingId()
    {
        this.mockRepo.Setup(r => r.GetByTrackingIdAsync(It.IsAny<TrackingId>()))
            .ReturnsAsync((Referral?)null);

        await Assert.ThrowsAsync<ArgumentException>(() => this.handler.Handle(new MarkInstalledCommand(Guid.NewGuid().ToString()), CancellationToken.None)).ConfigureAwait(true);
        this.mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Referral>()), Times.Never());
    }
}