namespace AppointmentConfirmation.API.Services;

public interface INotificationService
{
    Task SendNotificationToUserAsync(string message, params Guid[] userIds);
}
