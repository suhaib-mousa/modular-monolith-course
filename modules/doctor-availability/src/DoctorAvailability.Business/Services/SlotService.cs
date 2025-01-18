using DoctorAvailability.Business.DTOs;
using DoctorAvailability.Data.Repositories;
using DoctorAvailability.Domain.Common;
using DoctorAvailability.Domain.Entities;

namespace DoctorAvailability.Business.Services;

public class SlotService : ISlotService
{
    private readonly ISlotRepository _slotRepository;

    public SlotService(ISlotRepository slotRepository)
    {
        _slotRepository = slotRepository;
    }

    public async Task<Result<IEnumerable<Slot>>> GetAllSlotsAsync()
    {
        try
        {
            var slots = await _slotRepository.GetAllAsync();
            return Result<IEnumerable<Slot>>.Success(slots);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Slot>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Slot>>> GetAvailableSlotsAsync()
    {
        try
        {
            var slots = await _slotRepository.GetAvailableSlotsAsync();
            return Result<IEnumerable<Slot>>.Success(slots);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Slot>>.Failure(ex.Message);
        }
    }

    public async Task<Result<Slot>> GetSlotByIdAsync(Guid id)
    {
        try
        {
            var slot = await _slotRepository.GetByIdAsync(id);
            if (slot == null)
                return Result<Slot>.Failure("Slot not found");

            return Result<Slot>.Success(slot);
        }
        catch (Exception ex)
        {
            return Result<Slot>.Failure(ex.Message);
        }
    }

    public async Task<Result<Slot>> CreateSlotAsync(CreateSlotDto slotDto)
    {
        try
        {
            var slot = new Slot
            {
                Time = slotDto.Time,
                Cost = slotDto.Cost,
                IsReserved = false
            };

            var createdSlot = await _slotRepository.CreateAsync(slot);
            return Result<Slot>.Success(createdSlot);
        }
        catch (Exception ex)
        {
            return Result<Slot>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateSlotAsync(UpdateSlotDto slotDto)
    {
        try
        {
            var existingSlot = await _slotRepository.GetByIdAsync(slotDto.Id);
            if (existingSlot == null)
                return Result<bool>.Failure("Slot not found");

            existingSlot.Time = slotDto.Time;
            existingSlot.Cost = slotDto.Cost;
            existingSlot.IsReserved = slotDto.IsReserved;

            var result = await _slotRepository.UpdateAsync(existingSlot);
            return Result<bool>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteSlotAsync(Guid id)
    {
        try
        {
            var result = await _slotRepository.DeleteAsync(id);
            if (!result)
                return Result<bool>.Failure("Slot not found");

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task ReserveSlotAsync(Guid slotId)
    {
        var existingSlot = await _slotRepository.GetByIdAsync(slotId);
        if (existingSlot == null)
        {
            throw new Exception("Slot not found");
        }
        existingSlot.IsReserved = true;
        await _slotRepository.UpdateAsync(existingSlot);
    }
}