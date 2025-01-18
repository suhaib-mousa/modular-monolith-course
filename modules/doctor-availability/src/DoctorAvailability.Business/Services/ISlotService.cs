using DoctorAvailability.Business.DTOs;
using DoctorAvailability.Domain.Common;
using DoctorAvailability.Domain.Entities;

namespace DoctorAvailability.Business.Services;

public interface ISlotService
{
    Task<Result<IEnumerable<Slot>>> GetAllSlotsAsync();
    Task<Result<IEnumerable<Slot>>> GetAvailableSlotsAsync();
    Task<Result<Slot>> GetSlotByIdAsync(Guid id);
    Task<Result<Slot>> CreateSlotAsync(CreateSlotDto slotDto);
    Task<Result<bool>> UpdateSlotAsync(UpdateSlotDto slotDto);
    Task ReserveSlotAsync(Guid slotId);
    Task<Result<bool>> DeleteSlotAsync(Guid id);
}
