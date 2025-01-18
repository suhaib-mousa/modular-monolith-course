namespace AppointmentBooking.Application.Dtos;

public class BookAppointmentDto
{
    public Guid SlotId { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
}
