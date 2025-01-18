using AppointmentBooking.Domain.Entities;
using DoctorAvailability.Data;
using DoctorAvailability.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppointmentBooking.Infrastructure.Data;

public class AppointmentDbContext : DbContext, IAppointmentDbContext
{
    public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
        : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Slot> Slots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureAppointments();

        base.OnModelCreating(modelBuilder);
    }
}

public class AppointmentDbContextFactory : IDesignTimeDbContextFactory<AppointmentDbContext>
{
    public AppointmentDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppointmentDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppointmentBookingDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new AppointmentDbContext(optionsBuilder.Options);
    }
}