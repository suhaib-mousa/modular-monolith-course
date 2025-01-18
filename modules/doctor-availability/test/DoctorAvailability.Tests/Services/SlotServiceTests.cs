using DoctorAvailability.Business.DTOs;
using DoctorAvailability.Business.Services;
using DoctorAvailability.Data;
using DoctorAvailability.Data.Repositories;
using DoctorAvailability.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DoctorAvailability.Tests.Services;

public class SlotServiceTests
{
    private readonly SlotDbContext _context;
    private readonly ISlotRepository _repository;
    private readonly ISlotService _service;

    public SlotServiceTests()
    {
        var options = new DbContextOptionsBuilder<SlotDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SlotDbContext(options);
        _repository = new EfCoreSlotRepository(_context);
        _service = new SlotService(_repository);
    }

    [Fact]
    public async Task CreateSlot_ValidData_ReturnsSuccess()
    {
        // Arrange
        var createDto = new CreateSlotDto
        {
            Time = DateTime.Now.AddDays(1),
            DoctorId = Guid.NewGuid(),
            DoctorName = "Dr. Test",
            Cost = 100.00m
        };

        // Act
        var result = await _service.CreateSlotAsync(createDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(createDto.DoctorName, result.Value.DoctorName);
        Assert.False(result.Value.IsReserved);
    }

    [Fact]
    public async Task GetAvailableSlots_ReturnsOnlyUnreservedSlots()
    {
        // Arrange
        var slot1 = new Slot
        {
            Time = DateTime.Now.AddDays(1),
            DoctorId = Guid.NewGuid(),
            DoctorName = "Dr. Test",
            Cost = 100.00m,
            IsReserved = false
        };

        var slot2 = new Slot
        {
            Time = DateTime.Now.AddDays(2),
            DoctorId = Guid.NewGuid(),
            DoctorName = "Dr. Test",
            Cost = 100.00m,
            IsReserved = true
        };

        await _repository.CreateAsync(slot1);
        await _repository.CreateAsync(slot2);

        // Act
        var result = await _service.GetAvailableSlotsAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
        Assert.All(result.Value, slot => Assert.False(slot.IsReserved));
    }

    [Fact]
    public async Task UpdateSlot_ExistingSlot_ReturnsSuccess()
    {
        // Arrange
        var slot = new Slot
        {
            Time = DateTime.Now.AddDays(1),
            DoctorId = Guid.NewGuid(),
            DoctorName = "Dr. Test",
            Cost = 100.00m,
            IsReserved = false
        };
        await _repository.CreateAsync(slot);

        var updateDto = new UpdateSlotDto
        {
            Id = slot.Id,
            Time = DateTime.Now.AddDays(2),
            Cost = 150.00m,
            IsReserved = true
        };

        // Act
        var result = await _service.UpdateSlotAsync(updateDto);

        // Assert
        Assert.True(result.IsSuccess);
        var updatedSlot = await _repository.GetByIdAsync(slot.Id);
        Assert.NotNull(updatedSlot);
        Assert.Equal(updateDto.Time, updatedSlot.Time);
        Assert.Equal(updateDto.Cost, updatedSlot.Cost);
        Assert.True(updatedSlot.IsReserved);
    }

    [Fact]
    public async Task DeleteSlot_ExistingSlot_ReturnsSuccess()
    {
        // Arrange
        var slot = new Slot
        {
            Time = DateTime.Now.AddDays(1),
            DoctorId = Guid.NewGuid(),
            DoctorName = "Dr. Test",
            Cost = 100.00m
        };
        await _repository.CreateAsync(slot);

        // Act
        var result = await _service.DeleteSlotAsync(slot.Id);

        // Assert
        Assert.True(result.IsSuccess);
        var deletedSlot = await _repository.GetByIdAsync(slot.Id);
        Assert.Null(deletedSlot);
    }
}