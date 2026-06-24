using Microsoft.EntityFrameworkCore;
using VoterApi.Models;

namespace VoterApi.Data;

public class VoterDbContext : DbContext
{
    public VoterDbContext(DbContextOptions<VoterDbContext> options) : base(options)
    {
    }

    public DbSet<Voter> Voters => Set<Voter>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Voter>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.HasIndex(v => v.IdNumber).IsUnique();
        });

        SeedVoters(modelBuilder);
    }

    // Seed data ships inside the migration, so a fresh database comes up with
    // sample voters. Values are constant (no DateTime.UtcNow) as EF requires.
    private static void SeedVoters(ModelBuilder modelBuilder)
    {
        var seededAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Voter>().HasData(
            new Voter { Id = 1, IdNumber = "IDN0000001", FullName = "Asha Rao",     Age = 34, Gender = "Female", Address = "North Ward",   HasVoted = false, CreatedAt = seededAt, UpdatedAt = seededAt },
            new Voter { Id = 2, IdNumber = "IDN0000002", FullName = "Vikram Singh", Age = 45, Gender = "Male",   Address = "East Ward",    HasVoted = true,  CreatedAt = seededAt, UpdatedAt = seededAt },
            new Voter { Id = 3, IdNumber = "IDN0000003", FullName = "Meera Nair",   Age = 29, Gender = "Female", Address = "South Ward",   HasVoted = false, CreatedAt = seededAt, UpdatedAt = seededAt },
            new Voter { Id = 4, IdNumber = "IDN0000004", FullName = "Rahul Verma",  Age = 52, Gender = "Male",   Address = "West Ward",    HasVoted = true,  CreatedAt = seededAt, UpdatedAt = seededAt },
            new Voter { Id = 5, IdNumber = "IDN0000005", FullName = "Sana Khan",    Age = 38, Gender = "Female", Address = "Central Ward", HasVoted = false, CreatedAt = seededAt, UpdatedAt = seededAt },
            new Voter { Id = 6, IdNumber = "IDN0000006", FullName = "Arjun Mehta",  Age = 41, Gender = "Male",   Address = "North Ward",   HasVoted = true,  CreatedAt = seededAt, UpdatedAt = seededAt },
            new Voter { Id = 7, IdNumber = "IDN0000007", FullName = "Priya Iyer",   Age = 27, Gender = "Female", Address = "East Ward",    HasVoted = false, CreatedAt = seededAt, UpdatedAt = seededAt }
        );
    }
}
