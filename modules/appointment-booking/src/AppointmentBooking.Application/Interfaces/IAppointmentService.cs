using AppointmentBooking.Application.Dtos;
using AppointmentBooking.Domain.Entities;
using DoctorAvailability.Domain.Entities;

namespace AppointmentBooking.Application.Interfaces;

public interface IAppointmentService
{
    // FIXME the result class should be shared between modules
    Task<DoctorAvailability.Domain.Common.Result<IEnumerable<Slot>>> GetDoctorFreeSlotsAsync();
    Task<Result<AppointmentDto>> BookAppointmentAsync(BookAppointmentDto bookingDto);
    Task<Result<AppointmentDto>> GetAppointmentByIdAsync(Guid id);
    Task<Result<IEnumerable<AppointmentDto>>> GetPatientAppointmentsAsync(Guid patientId);
}
