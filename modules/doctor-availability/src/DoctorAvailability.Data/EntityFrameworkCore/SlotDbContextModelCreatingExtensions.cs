using DoctorAvailability.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailability.Data.EntityFrameworkCore;

public static class SlotDbContextModelCreatingExtensions
{
    public static void ConfigureSlots(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DoctorName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Cost).HasPrecision(18, 2);
            entity.Property(e => e.Time).IsRequired();
        });
    }
}
