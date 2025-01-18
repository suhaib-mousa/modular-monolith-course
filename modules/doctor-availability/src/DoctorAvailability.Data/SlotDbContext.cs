using DoctorAvailability.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DoctorAvailability.Data;

public class SlotDbContext : DbContext
{
    public SlotDbContext(DbContextOptions<SlotDbContext> options) : base(options)
    {
    }

    public DbSet<Slot> Slots => Set<Slot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DoctorName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Cost).HasPrecision(18, 2);
            entity.Property(e => e.Time).IsRequired();
        });

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