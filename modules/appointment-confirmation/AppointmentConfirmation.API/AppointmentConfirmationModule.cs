using AppointmentConfirmation.API.Services;

namespace AppointmentConfirmation.API;

public static class AppointmentConfirmationModule
{
    public static void AddAppointmentConfirmation(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, LoggerNotificationService>();
    }
}
