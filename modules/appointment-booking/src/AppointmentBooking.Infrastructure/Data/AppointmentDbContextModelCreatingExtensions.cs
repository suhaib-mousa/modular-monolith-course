using AppointmentBooking.Domain.Entities;
using DoctorAvailability.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Data;

public static class AppointmentDbContextModelCreatingExtensions
{
    public static void ConfigureAppointments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PatientName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ReservedAt).IsRequired();
            entity.Property(e => e.Status).IsRequired();
        });

        modelBuilder.ConfigureSlots();
    }
}
