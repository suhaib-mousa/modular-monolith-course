using AppointmentBooking.Domain.Entities;

namespace AppointmentBooking.Domain.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);
    Task<bool> UpdateAsync(Appointment appointment);
}