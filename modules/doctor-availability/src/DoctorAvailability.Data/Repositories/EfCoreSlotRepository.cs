﻿using DoctorAvailability.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailability.Data.Repositories;

public class EfCoreSlotRepository : ISlotRepository
{
    private readonly SlotDbContext _context;

    public EfCoreSlotRepository(SlotDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Slot>> GetAllAsync()
    {
        return await _context.Slots.AsNoTracking().ToListAsync();
    }
    public async Task<IEnumerable<Slot>> GetReservedSlotsAsync()
    {
        var query = _context.Slots.AsQueryable().AsNoTracking();
        return await query.Where(s => s.IsReserved).ToListAsync();
    }
    public async Task<Slot?> GetByIdAsync(Guid id)
    {
        return await _context.Slots.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task<IEnumerable<Slot>> GetAvailableSlotsAsync()
    {
        var query = _context.Slots.AsQueryable().AsNoTracking();
        return await query.Where(s => !s.IsReserved).ToListAsync();
    }

    public async Task<Slot> CreateAsync(Slot slot)
    {
        _context.Slots.Add(slot);
        await _context.SaveChangesAsync();
        return slot;
    }

    public async Task<bool> UpdateAsync(Slot slot)
    {
        try
        {
            _context.Entry(slot).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var slot = await _context.Slots.FindAsync(id);
        if (slot == null)
            return false;

        _context.Slots.Remove(slot);
        await _context.SaveChangesAsync();
        return true;
    }

   
}