using DoctorAvailability.Business.Services;
using DoctorAvailability.Data;
using DoctorAvailability.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailability.API;

public static class DoctorAvailabilityModule
{
    public static void AddDoctorAvailabilityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SlotDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
        ));
        services.AddScoped<ISlotRepository, EfCoreSlotRepository>();
        services.AddScoped<ISlotService, SlotService>();
    }
}
