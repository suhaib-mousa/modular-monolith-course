using AppointmentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Ports.Interfaces
{
    public interface IAppointmentsManagementPort
    {
        Task UpdateAppointmentStatusAsync(DoctorAppointmentDto appointmentDto);
        public Task<IEnumerable<DoctorAppointmentDto>> GetUpcomingAppointmentsAsync();
    }
}
