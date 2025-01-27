using AppointmentBooking.Application.Dtos;
using AppointmentBooking.Application.Interfaces;
using AppointmentBooking.Domain.Entities;
using AppointmentManagement.Core.Entities;
using AppointmentManagement.Ports.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.Adapter.Adapters
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsManagementAdapter : ControllerBase,IAppointmentsManagementPort
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsManagementAdapter(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet("get-upcoming-appointments")]
        public async Task<IEnumerable<DoctorAppointmentDto>> GetUpcomingAppointmentsAsync()
        {
            var upcommingAppointments= (await _appointmentService.GetUpcomingAppointmentsAsync()).Value;
            return upcommingAppointments.Select(appointment => new DoctorAppointmentDto
            {
                Id = appointment.Id,
                SlotId = appointment.SlotId,
                PatientId = appointment.PatientId,
                PatientName = appointment.PatientName,
                ReservedAt = appointment.ReservedAt,
                Status = appointment.Status.ToString() // Convert Enum to string
            });

        }
        [HttpPost("update-appointment-status")]
        public async Task UpdateAppointmentStatusAsync(DoctorAppointmentDto doctorAppointmentDto)
        {

            if (Enum.TryParse<AppointmentStatus>(doctorAppointmentDto.Status, out var newStatus))
            {
                // Use newStatus here
                var appointmentsDto = new AppointmentDto
                {
                    Id = doctorAppointmentDto.Id,
                    SlotId = doctorAppointmentDto.SlotId,
                    PatientId = doctorAppointmentDto.PatientId,
                    PatientName = doctorAppointmentDto.PatientName,
                    ReservedAt = doctorAppointmentDto.ReservedAt,
                    Status = newStatus // Convert Enum to string
                };
                await _appointmentService.UpdateAppointmentStatusAsync(appointmentsDto);
            }
            
        }
    }
}
