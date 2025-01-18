namespace DoctorAvailability.Domain.Entities;
public class Slot
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public Guid DoctorId { get; private set; } = Guid.Parse("0166ed28-81c6-4c3d-b0a1-a96b8459788e");
    public string DoctorName { get; private set; } = "Dr Ahmad";
    public bool IsReserved { get; set; }
    public decimal Cost { get; set; }
}