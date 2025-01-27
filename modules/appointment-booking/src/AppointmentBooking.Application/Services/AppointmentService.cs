using AppointmentBooking.Application.Dtos;
using AppointmentBooking.Application.Interfaces;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Repositories;
using AppointmentConfirmation.API.Services;
using DoctorAvailability.Business.Services;
using DoctorAvailability.Domain.Entities;

namespace AppointmentBooking.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ISlotService _slotService;
    private readonly INotificationService _notificationService;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        ISlotService slotService,
        INotificationService notificationService)
    {
        _appointmentRepository = appointmentRepository;
        _slotService = slotService;
        _notificationService = notificationService;
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

            var slot = slotResult.Value;

            // Reserve the slot
            await _slotService.ReserveSlotAsync(slotId: slot.Id);

            // Create appointment
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                SlotId = slot.Id,
                PatientId = bookingDto.PatientId,
                PatientName = bookingDto.PatientName,
                ReservedAt = DateTime.UtcNow,
                Status = AppointmentStatus.Reserved
            };

            var createdAppointment = await _appointmentRepository.CreateAsync(appointment);

            var appointmentDto = MapToDto(createdAppointment);

            // Send confirmation notification
            var message = GenerateNotificaionMessage(appointmentDto, slot.DoctorName);
            await _notificationService.SendNotificationToUserAsync(message, userIds: [appointment.PatientId, slot.DoctorId]);

            return Result<AppointmentDto>.Success(appointmentDto);
        }
        catch (Exception ex)
        {
            return Result<AppointmentDto>.Failure(ex.Message);
        }
    }


    public async Task<Result<AppointmentDto>> UpdateAppointmentStatusAsync(AppointmentDto appointmentDto)
    {
        try
        {
            // Get slot details and verify availability
            var slotResult = await _slotService.GetSlotByIdAsync(appointmentDto.SlotId);
            if (!slotResult.IsSuccess)
                return Result<AppointmentDto>.Failure(slotResult.Error ?? "Slot not found");

            var slot = slotResult.Value;
            // Reserve the slot
            if (appointmentDto.Status == AppointmentStatus.Cancelled)
            {
                await _slotService.ChangeSlotAvailabiltyAsync(slotId: slot.Id);
            }
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentDto.Id);

            // Create appointment
            appointment.Status = appointmentDto.Status;

            var createdAppointment = await _appointmentRepository.UpdateAsync(appointment);

            //var appointmentDto = MapToDto(createdAppointment);

            // Send confirmation notification
            var message = GenerateNotificaionMessage(appointmentDto, slot.DoctorName);
            await _notificationService.SendNotificationToUserAsync(message, userIds: [appointment.PatientId, slot.DoctorId]);

            return Result<AppointmentDto>.Success(appointmentDto);
        }
        catch (Exception ex)
        {
            return Result<AppointmentDto>.Failure(ex.Message);
        }
    }
    private string GenerateNotificaionMessage(AppointmentDto appointmentDto, string doctorName)
    {
        return $@"Dear {appointmentDto.PatientName},
    
            Your appointment has been confirmed with Dr. {doctorName}.
            Details are as follows:
            - Appointment Time: {appointmentDto.ReservedAt:dddd, MMMM dd, yyyy hh:mm tt}
            - Appointment Status: {appointmentDto.Status}

            Thank you for choosing our services.";
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
            Status = appointment.Status
        };
    }

    public async Task<DoctorAvailability.Domain.Common.Result<IEnumerable<Slot>>> GetDoctorFreeSlotsAsync()
    {
        return await _slotService.GetAvailableSlotsAsync();
    }

    public async Task<Result<IEnumerable<AppointmentDto>>> GetUpcomingAppointmentsAsync()
    {
        var upcomingAppointments= await _appointmentRepository.GetUpcomingAppointmentsAsync();
        var upcomingAppointmentDtos = upcomingAppointments.Select(appointment => new AppointmentDto
        {
            Id = appointment.Id,
            SlotId = appointment.SlotId,
            PatientId = appointment.PatientId,
            PatientName = appointment.PatientName,
            ReservedAt = appointment.ReservedAt,
            Status = appointment.Status // Convert Enum to string
        });
        return Result<IEnumerable<AppointmentDto>>.Success(upcomingAppointmentDtos);
    }
}
