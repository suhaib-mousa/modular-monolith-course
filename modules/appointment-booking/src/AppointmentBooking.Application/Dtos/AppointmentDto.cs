namespace AppointmentBooking.Application.Dtos;

public class AppointmentDto
{
    public Guid Id { get; set; }
    public Guid SlotId { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public DateTime ReservedAt { get; set; }
    public string Status { get; set; } = string.Empty;
}