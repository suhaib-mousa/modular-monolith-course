using AppointmentBooking.Application.Interfaces;
using AppointmentBooking.Application.Services;
using AppointmentBooking.Domain.Repositories;
using AppointmentBooking.Infrastructure.Data;
using AppointmentBooking.Infrastructure.Repositories;
using AppointmentConfirmation.API;
using DoctorAvailability.API;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.API;

public static class AppointmentBookingModule
{
    public static void AddAppointmentBooking(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppointmentDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
        ));

        services.AddScoped<IAppointmentRepository, EfCoreAppointmentRepository>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddDoctorAvailabilityModule(configuration);
        services.AddAppointmentConfirmation();
    }
}
