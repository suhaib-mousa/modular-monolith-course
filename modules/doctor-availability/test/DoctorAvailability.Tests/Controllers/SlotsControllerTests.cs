using DoctorAvailability.API.Controllers;
using DoctorAvailability.Business.Services;
using DoctorAvailability.Domain.Common;
using DoctorAvailability.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DoctorAvailability.Tests.Controllers;

public class SlotsControllerTests
{
    private readonly Mock<ISlotService> _mockSlotService;
    private readonly SlotsController _controller;

    public SlotsControllerTests()
    {
        _mockSlotService = new Mock<ISlotService>();
        _controller = new SlotsController(_mockSlotService.Object);
    }

    [Fact]
    public async Task GetAllSlots_ReturnsOkResult()
    {
        // Arrange
        var slots = new List<Slot> { new Slot { Id = Guid.NewGuid() } };
        _mockSlotService.Setup(s => s.GetAllSlotsAsync())
            .ReturnsAsync(Result<IEnumerable<Slot>>.Success(slots));

        // Act
        var result = await _controller.GetAllSlots();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedSlots = Assert.IsAssignableFrom<IEnumerable<Slot>>(okResult.Value);
        Assert.Single(returnedSlots);
    }

    [Fact]
    public async Task GetAvailableSlots_ReturnsOkResult()
    {
        // Arrange
        var slots = new List<Slot> { new Slot { Id = Guid.NewGuid(), IsReserved = false } };
        _mockSlotService.Setup(s => s.GetAvailableSlotsAsync())
            .ReturnsAsync(Result<IEnumerable<Slot>>.Success(slots));

        // Act
        var result = await _controller.GetAvailableSlots();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedSlots = Assert.IsAssignableFrom<IEnumerable<Slot>>(okResult.Value);
        Assert.Single(returnedSlots);
    }
}