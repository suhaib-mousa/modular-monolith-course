using AppointmentBooking.Domain.Entities;
using DoctorAvailability.Data;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Data;

public interface IAppointmentDbContext : ISlotDbContext
{
    public DbSet<Appointment> Appointments {  get; }
}