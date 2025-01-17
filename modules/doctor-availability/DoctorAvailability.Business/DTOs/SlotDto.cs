namespace DoctorAvailability.Business.DTOs;

public class CreateSlotDto
{
    public DateTime Time { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public decimal Cost { get; set; }
}

public class UpdateSlotDto
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public decimal Cost { get; set; }
    public bool IsReserved { get; set; }
}