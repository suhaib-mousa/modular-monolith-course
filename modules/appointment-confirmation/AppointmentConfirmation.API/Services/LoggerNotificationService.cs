namespace AppointmentConfirmation.API.Services;

public class LoggerNotificationService : INotificationService
{
    public LoggerNotificationService(ILogger<LoggerNotificationService> logger)
    {
        Logger = logger;
    }

    public ILogger<LoggerNotificationService> Logger { get; }

    public async Task SendNotificationToUserAsync(string message, params Guid[] userIds)
    {
        foreach (var userId in userIds)
        {
            Logger.LogInformation($"Notification sent to user {userId}: {message}");
        }
        await Task.CompletedTask;
    }
}
