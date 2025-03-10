﻿using DoctorAvailability.Business.DTOs;
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

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllSlots()
    {
        var result = await _slotService.GetAllSlotsAsync();
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("get-reserved-slots")]
    public async Task<IActionResult> GetReservedSlots()
    {
        var result = await _slotService.GetReservedSlotsAsync();
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("create-slot")]
    public async Task<IActionResult> CreateSlot(CreateSlotDto slotDto)
    {
        var result = await _slotService.CreateSlotAsync(slotDto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Value);
    }
    [HttpPut("update-slot")]
    public async Task<IActionResult> UpdateSlot(UpdateSlotDto slotDto)
    {
        var result = await _slotService.UpdateSlotAsync(slotDto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        return Ok(result.Value);
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