namespace DoctorAvailability.Domain.Entities;
public class Slot
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public bool IsReserved { get; set; }
    public decimal Cost { get; set; }
}