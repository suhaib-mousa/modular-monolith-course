using DoctorAvailability.Business.DTOs;
using DoctorAvailability.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAvailability.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SlotsController : ControllerBase
{
    private readonly ISlotService _slotService;

    public SlotsController(ISlotService slotService)
    {
        _slotService = slotService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSlots()
    {
        var result = await _slotService.GetAllSlotsAsync();
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableSlots()
    {
        var result = await _slotService.GetAvailableSlotsAsync();
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSlot(Guid id)
    {
        var result = await _slotService.GetSlotByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSlot(CreateSlotDto slotDto)
    {
        var result = await _slotService.CreateSlotAsync(slotDto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetSlot), new { id = result.Value?.Id }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSlot(Guid id, UpdateSlotDto slotDto)
    {
        if (id != slotDto.Id)
            return BadRequest("ID mismatch");

        var result = await _slotService.UpdateSlotAsync(slotDto);
        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSlot(Guid id)
    {
        var result = await _slotService.DeleteSlotAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }
}