using AppointmentBooking.Application.Dtos;
using AppointmentBooking.Application.Interfaces;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Repositories;
using DoctorAvailability.Business.DTOs;
using DoctorAvailability.Business.Services;
using DoctorAvailability.Domain.Entities;

namespace AppointmentBooking.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ISlotService _slotService;
    private readonly object _notificationService; // temp

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        ISlotService slotService)
    {
        _appointmentRepository = appointmentRepository;
        _slotService = slotService;
        //_notificationService = notificationService;
    }

    public async Task<Result<AppointmentDto>> BookAppointmentAsync(BookAppointmentDto bookingDto)
    {
        try
        {
            // Get slot details and verify availability
            var slotResult = await _slotService.GetSlotByIdAsync(bookingDto.SlotId);
            if (!slotResult.IsSuccess)
                return Result<AppointmentDto>.Failure(slotResult.Error ?? "Slot not found");

            if (slotResult.Value!.IsReserved)
                return Result<AppointmentDto>.Failure("Slot is already reserved");

            // Reserve the slot
            await _slotService.ReserveSlotAsync(slotId: bookingDto.SlotId);

            // Create appointment
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                SlotId = bookingDto.SlotId,
                PatientId = bookingDto.PatientId,
                PatientName = bookingDto.PatientName,
                ReservedAt = DateTime.UtcNow,
                Status = AppointmentStatus.Reserved
            };

            var createdAppointment = await _appointmentRepository.CreateAsync(appointment);

            var appointmentDto = MapToDto(createdAppointment);

            // Send confirmation notification ~ TODO
            //await _notificationService.SendAppointmentConfirmationAsync(appointmentDto, slotResult.Value);

            return Result<AppointmentDto>.Success(appointmentDto);
        }
        catch (Exception ex)
        {
            return Result<AppointmentDto>.Failure(ex.Message);
        }
    }

    public async Task<Result<AppointmentDto>> GetAppointmentByIdAsync(Guid id)
    {
        try
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
                return Result<AppointmentDto>.Failure("Appointment not found");

            return Result<AppointmentDto>.Success(MapToDto(appointment));
        }
        catch (Exception ex)
        {
            return Result<AppointmentDto>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<AppointmentDto>>> GetPatientAppointmentsAsync(Guid patientId)
    {
        try
        {
            var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);
            var appointmentDtos = appointments.Select(MapToDto);
            return Result<IEnumerable<AppointmentDto>>.Success(appointmentDtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<AppointmentDto>>.Failure(ex.Message);
        }
    }

    private static AppointmentDto MapToDto(Appointment appointment)
    {
        return new AppointmentDto
        {
            Id = appointment.Id,
            SlotId = appointment.SlotId,
            PatientId = appointment.PatientId,
            PatientName = appointment.PatientName,
            ReservedAt = appointment.ReservedAt,
            Status = appointment.Status.ToString()
        };
    }

    public async Task<DoctorAvailability.Domain.Common.Result<IEnumerable<Slot>>> GetDoctorFreeSlotsAsync()
    {
        return await _slotService.GetAvailableSlotsAsync();
    }
}
