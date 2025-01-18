namespace DoctorAvailability.Business.DTOs;

public class CreateSlotDto
{
    public DateTime Time { get; set; }
    public decimal Cost { get; set; }
}

public class UpdateSlotDto
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public decimal Cost { get; set; }
    public bool IsReserved { get; set; }
}