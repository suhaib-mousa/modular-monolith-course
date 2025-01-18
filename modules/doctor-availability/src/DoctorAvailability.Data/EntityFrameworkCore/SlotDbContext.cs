using DoctorAvailability.Data.EntityFrameworkCore;
using DoctorAvailability.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DoctorAvailability.Data;

public class SlotDbContext : DbContext, ISlotDbContext
{
    public SlotDbContext(DbContextOptions<SlotDbContext> options) : base(options)
    {
    }

    public DbSet<Slot> Slots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureSlots();

        base.OnModelCreating(modelBuilder);
    }
}
public class SlotDbContextFactory : IDesignTimeDbContextFactory<SlotDbContext>
{
    public SlotDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SlotDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DoctorAvailabilityDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new SlotDbContext(optionsBuilder.Options);
    }
}