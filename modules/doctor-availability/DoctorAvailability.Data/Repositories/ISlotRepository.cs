using DoctorAvailability.Domain.Entities;

namespace DoctorAvailability.Data.Repositories;


public interface ISlotRepository
{
    Task<IEnumerable<Slot>> GetAllAsync();
    Task<Slot?> GetByIdAsync(Guid id);
    Task<IEnumerable<Slot>> GetAvailableSlotsAsync();
    Task<Slot> CreateAsync(Slot slot);
    Task<bool> UpdateAsync(Slot slot);
    Task<bool> DeleteAsync(Guid id);
}
