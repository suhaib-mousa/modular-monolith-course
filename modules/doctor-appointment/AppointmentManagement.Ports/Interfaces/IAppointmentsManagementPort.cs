using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Ports.Interfaces
{
    public interface IAppointmentsManagementPort
    {
        Task<AppointmentDto> UpdateAppointmentStatusAsync(AppointmentDto appointmentDto);
    }
}
