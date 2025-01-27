using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Repositories;
using AppointmentBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Repositories;

public class EfCoreAppointmentRepository : IAppointmentRepository
{
    private readonly AppointmentDbContext _context;

    public EfCoreAppointmentRepository(AppointmentDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {
        return await _context.Appointments.FindAsync(id);
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.Appointments
            .Where(a => a.PatientId == patientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync()//to do: get the data according to a specific doctor
    {
        return await _context.Appointments.Where(x=>x.Status== AppointmentStatus.Reserved).AsNoTracking().ToListAsync();//suppose we have just one doctor
    }

    public async Task<bool> UpdateAsync(Appointment appointment)
    {
        try
        {
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }
}