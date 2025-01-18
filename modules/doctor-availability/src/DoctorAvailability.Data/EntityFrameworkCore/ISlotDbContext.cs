using DoctorAvailability.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailability.Data;

public interface ISlotDbContext
{
    public DbSet<Slot> Slots { get; }
}