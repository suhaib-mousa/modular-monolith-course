using AppointmentBooking.API;
using AppointmentBooking.Application.Interfaces;
using AppointmentBooking.Application.Services;
using AppointmentBooking.Domain.Repositories;
using AppointmentBooking.Infrastructure.Data;
using AppointmentBooking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Adapter
{

    public static class AppointmentManagementModule
    {
        public static void AddAppointmentManagement(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppointmentBooking(configuration);
        }

    }
}
