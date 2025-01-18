using AppointmentBooking.Application.Dtos;
using AppointmentBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost("book-appointment")]
    public async Task<IActionResult> BookAppointment(BookAppointmentDto bookingDto)
    {
        var result = await _appointmentService.BookAppointmentAsync(bookingDto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientAppointments(Guid patientId)
    {
        var result = await _appointmentService.GetPatientAppointmentsAsync(patientId);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("doctor-free-slots")]
    public async Task<IActionResult> GetAba()
    {
        var result = await _appointmentService.GetDoctorFreeSlotsAsync();
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
